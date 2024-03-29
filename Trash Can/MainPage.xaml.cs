﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Trash_Can.Controls;
using Trash_Can.Elements;
using Trash_Can.Trashs;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Trash_Can
{
    public sealed partial class MainPage : Page
    {

        //@Converter
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanToVisibilityConverter(bool value) => value ? Visibility.Collapsed : Visibility.Visible;
        private Symbol ReverseBooleanToPinConverter(bool value) => value ? Symbol.UnPin : Symbol.Pin;
        private bool StringToBooleanConverter(string value) => string.IsNullOrEmpty(value) == false;


        public LoadingState State { get => this.LoadingControl.State; set => this.LoadingControl.State = value; }
        public string Title { get => this.TitleTextBlock.Text; set => this.TitleTextBlock.Text = value; }
        public string Subtitle { get => this.SubtitleTextBlock.Text; set => this.SubtitleTextBlock.Text = value; }
        public ITextDocument Document => this.RichEditBox.Document;
        public bool HasChanged => this.Document.CanUndo() || this.Document.CanRedo();
        public ITextRange Selection => this.Document.Selection;
        public ITextCharacterFormat CharacterFormat { get => this.Selection.CharacterFormat; set => this.Selection.CharacterFormat = value; }
        public ITextParagraphFormat ParagraphFormat { get => this.Selection.ParagraphFormat; set => this.Selection.ParagraphFormat = value; }


        public readonly ObservableCollection<TrashItem> Items = new ObservableCollection<TrashItem>();


        //@VisualState
        bool _vsIsWritable = false;
        bool _vsIsFullScreen = false;
        DeviceLayoutType _vsDeviceLayoutType = DeviceLayoutType.PC;
        /// <summary> 
        /// Represents the visual appearance of UI elements in a specific state.
        /// </summary>
        public VisualState VisualState
        {
            get
            {
                if (this._vsIsFullScreen)
                {
                    switch (this._vsDeviceLayoutType)
                    {
                        case DeviceLayoutType.PC: return this.FullScreenPC;
                        case DeviceLayoutType.Pad: return this.FullScreenPad;
                        case DeviceLayoutType.Phone: return this.WritablePhone;
                    }
                }
                else if (this._vsIsWritable)
                {
                    switch (this._vsDeviceLayoutType)
                    {
                        case DeviceLayoutType.PC: return this.WritablePC;
                        case DeviceLayoutType.Pad: return this.WritablePad;
                        case DeviceLayoutType.Phone: return this.WritablePhone;
                    }
                }
                else
                {
                    switch (this._vsDeviceLayoutType)
                    {
                        case DeviceLayoutType.PC: return this.PC;
                        case DeviceLayoutType.Pad: return this.Pad;
                        case DeviceLayoutType.Phone: return this.Phone;
                    }
                }

                return this.PC;
            }
            set => VisualStateManager.GoToState(this, value.Name, true);
        }
        DeviceLayout DeviceLayout = DeviceLayout.Default;


        public MainPage()
        {
            this.InitializeComponent();
            this.ConstructFlowDirection();
            this.ConstructStrings();

            this.ConstructHead();
            this.ConstructEdit();
            this.ConstructFont();
            this.ConstructFontFamily();
            this.ConstructFontSize();
            this.ConstructTagType();
            this.ConstructFind();


            // Extend TitleBar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;


            // Drag and Drop 
            base.AllowDrop = true;
            base.Drop += async (s, e) =>
            {
                this.State = LoadingState.Loading;
                if (e.DataView.Contains(StandardDataFormats.StorageItems))
                {
                    IReadOnlyList<IStorageItem> items = await e.DataView.GetStorageItemsAsync();
                    foreach (IStorageItem item in items)
                    {
                        bool result = await this.Activated(item);
                        if (result == false)
                        {
                            this.State = LoadingState.FileCorrupt;
                            await Task.Delay(1000);
                        }
                        break;
                    }
                }
                this.State = LoadingState.None;
            };
            base.DragOver += (s, e) =>
            {
                e.AcceptedOperation = DataPackageOperation.Copy;
                //e.DragUIOverride.Caption = 
                e.DragUIOverride.IsCaptionVisible = e.DragUIOverride.IsContentVisible = e.DragUIOverride.IsGlyphVisible = true;
            };


            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this._vsDeviceLayoutType = this.DeviceLayout.GetActualType(e.NewSize.Width);
                this.VisualState = this.VisualState; // VisualState
            };
            base.Loaded += async (s, e) =>
            {
                if (this.Items.Count != 0) return;

                IEnumerable<TrashItem> trashs = await XML.ConstructTrashsFile();
                if (trashs == null)
                {
                    await this.New(this.Untitled);
                }
                else
                {
                    foreach (TrashItem item in trashs)
                    {
                        this.Items.Add(item);
                    }
                }

                this.FlipView.SelectedIndex = 0;
            };


            // SplitButton
            this.PhoneSplitButton.Click += (s, e) => this.SplitView.IsPaneOpen = true;
            this.PCSplitButton.Click += (s, e) =>
            {
                switch (this.SplitView.DisplayMode)
                {
                    case SplitViewDisplayMode.Overlay:
                        this.SplitView.IsPaneOpen = false;
                        break;
                    case SplitViewDisplayMode.CompactOverlay:
                    case SplitViewDisplayMode.CompactInline:
                        this.SplitView.IsPaneOpen = !this.SplitView.IsPaneOpen;
                        break;
                }
            };


            this.AddButton.Click += async (s, e) =>
            {
                this.State = LoadingState.Loading;
                {
                    TrashItem trash = await this.New(this.Untitled);
                    bool result = await this.Open(trash, base.ActualTheme);
                    if (result == false)
                    {
                        this.State = LoadingState.LoadFailed;
                        await Task.Delay(1000);
                    }
                }
                this.State = LoadingState.None;
            };
            this.SettingItem.Tapped += async (s, e) => await this.SettingDialog.ShowInstance();
            this.PaneListView.ItemClick += async (s, e) =>
            {
                if (e.ClickedItem is StackPanel stackPanel)
                {
                    if (stackPanel.Tag is int index)
                    {
                        switch (index)
                        {
                            case 0:
                                {
                                    this.State = LoadingState.Loading;
                                    {
                                        TrashItem trash = await this.New(this.Untitled);
                                        bool result = await this.Open(trash, base.ActualTheme);
                                        if (result == false)
                                        {
                                            this.State = LoadingState.LoadFailed;
                                            await Task.Delay(1000);
                                        }
                                    }
                                    this.State = LoadingState.None;
                                }
                                break;
                            case 1:
                                switch (this._vsDeviceLayoutType)
                                {
                                    case DeviceLayoutType.Phone:
                                    case DeviceLayoutType.Pad:
                                        this.SplitView.IsPaneOpen = false;
                                        break;
                                    default:
                                        break;
                                }
                                if (this._vsIsWritable)
                                {
                                    switch (this._vsDeviceLayoutType)
                                    {
                                        case DeviceLayoutType.Phone:
                                            break;
                                        default:
                                            this.State = LoadingState.Saving;
                                            {
                                                string name = this.Subtitle;
                                                if (this.Items.FirstOrDefault(c => c.Name == name) is TrashItem item)
                                                {
                                                    bool result = await this.Exit(item, base.ActualTheme);
                                                    if (result == false)
                                                    {
                                                        this.State = LoadingState.SaveFailed;
                                                        await Task.Delay(1000);
                                                    }
                                                }
                                            }
                                            this.State = LoadingState.None;
                                            break;
                                    }
                                }
                                break;
                            case 2:
                                await this.AboutDialog.ShowInstance();
                                break;
                            default:
                                break;
                        }
                    }
                }
            };


            // Color:
            // The SelectionHighlightColor ignore Focuse.
            this.RichEditBox.SelectionHighlightColorWhenNotFocused = this.RichEditBox.SelectionHighlightColor;

            // Foregournd follow Theme
            this.RichEditBox.RequestedTheme = base.ActualTheme;
            base.ActualThemeChanged += (s, e) =>
            {
                this.Document.GetText(TextGetOptions.FormatRtf, out string text);
                {
                    ElementTheme theme = base.ActualTheme;
                    {
                        text = FileUtil.ReadTextAsync(text, theme);
                    }
                    this.RichEditBox.RequestedTheme = theme;
                }
                this.Document.SetText(TextSetOptions.FormatRtf, text);
            };

            this.RichEditBox.SelectionFlyout = null;
            this.RichEditBox.TextCompositionEnded += (s, e) => this.Update();
            this.RichEditBox.CopyingToClipboard += (s, e) => this.Update();
            this.RichEditBox.CuttingToClipboard += (s, e) => this.Update();
            this.RichEditBox.SelectionChanging += (s, e) => this.Update();

            // Text:
            // The FontFamily, FontSize follow Setting.
            this.RichEditBox.FontFamily = new FontFamily(this.SettingDialog.GetFontFamily());
            this.RichEditBox.FontSize = this.SettingDialog.GetFontSize();


            // Setting
            this.RichEditBox.FontFamily = new FontFamily(this.SettingDialog.GetFontFamily());
            this.RichEditBox.FontSize = this.SettingDialog.GetFontSize();
            Thickness pageMargin1 = this.SettingDialog.GetPageMargin();
            this.UpdateRichEditBoxPadding(this.FindMode, pageMargin1);

            this.SettingDialog.FontFamilySetted += (s, fontFamily) => this.RichEditBox.FontFamily = new FontFamily(fontFamily);
            this.SettingDialog.FontSizeSetted += (s, fontSize) => this.RichEditBox.FontSize = fontSize;
            this.SettingDialog.PageMarginSetted += (s, pageMargin) => this.UpdateRichEditBoxPadding(this.FindMode, pageMargin);
            this.SettingDialog.LanguageSetted += (s, language) =>
            {
                this.ConstructFlowDirection();
                this.ConstructStrings();
            };


            this.Editor.PrimaryButtonClick += (s, e) =>
            {
                string name = this.Editor.Subtitle;
                TrashItem item = string.IsNullOrEmpty(name) ? null : this.Items.FirstOrDefault(c => c.Name == name);
                if (item == null) return;

                item.Properties = this.Editor.Trash;
                this.Save();

                if (this._vsIsWritable) this.Title = item.Title;
            };
        }

    }

    public sealed partial class MainPage : Page
    {

        //@BackRequested
        /// <summary> The current page no longer becomes an active page. </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            TrashControl.Operated -= this.TrashControl_OperatedAsync;
        }
        /// <summary> The current page becomes the active page. </summary>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            TrashControl.Operated += this.TrashControl_OperatedAsync;

            if (e.Parameter is IStorageItem item)
            {
                bool result = false;
                this.State = LoadingState.Loading;
                {
                    // 1. FInd in Local Folder.
                    foreach (TrashItem item2 in this.Items)
                    {
                        if (item2.Name == item.Name)
                        {
                            result = await this.Open(item2, base.ActualTheme);
                        }
                    }

                    // 2. Activate new File.
                    if (result == false) result = await this.Activated(item);

                    if (result == false)
                    {
                        this.State = LoadingState.FileCorrupt;
                        await Task.Delay(1000);
                    }
                }
                this.State = LoadingState.None;
            }
        }

        private async void TrashControl_OperatedAsync(object sender, OperateType e)
        {
            if (sender is TrashItem item)
            {
                switch (e)
                {
                    case OperateType.Edit:
                        {
                            this.State = LoadingState.Loading;
                            {
                                bool result = await this.Open(item, base.ActualTheme);
                                if (result == false)
                                {
                                    this.State = LoadingState.FileCorrupt;
                                    await Task.Delay(1000);
                                }
                            }
                            this.State = LoadingState.None;
                        }
                        break;
                    case OperateType.Rename:
                        {
                            this.Editor.Subtitle = item.Name;
                            this.Editor.Trash = item.Properties;
                            await this.Editor.ShowInstance();
                        }
                        break;
                    case OperateType.Remove:
                        {
                            if (item.IsFavorite) return;

                            this.State = LoadingState.Loading;
                            {
                                StorageFile file = await FileUtil.FIndRtfFile(item.Name);
                                if (file == null)
                                {
                                    this.State = LoadingState.FileNull;
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    await file.DeleteAsync();
                                }
                            }
                            this.State = LoadingState.None;

                            int index = this.FlipView.SelectedIndex;
                            this.Items.Remove(item);
                            this.Save();
                            this.FlipView.SelectedIndex = Math.Max(0, index - 1);
                        }
                        break;
                    case OperateType.Duplicate:
                        {
                            this.State = LoadingState.Loading;
                            {
                                TrashItem trash = await FileUtil.CopyRtfFile(item.Name, base.ActualTheme);
                                if (trash == null)
                                {
                                    this.State = LoadingState.FileNull;
                                    await Task.Delay(1000);
                                }
                                else
                                {
                                    trash.Properties = item.Properties.Clone();
                                    this.Items.Add(trash);
                                    this.Save();
                                }
                            }
                            this.State = LoadingState.None;
                        }
                        break;
                    case OperateType.Favorite:
                        {
                            item.IsFavorite = true;
                            this.Save();
                        }
                        break;
                    case OperateType.UnFavorite:
                        {
                            item.IsFavorite = false;
                            this.Save();
                        }
                        break;
                    case OperateType.LocalFolder:
                        {
                            await this.LaunchFolder(item.Name);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}