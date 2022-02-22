using System;
using System.Linq;
using System.Threading.Tasks;
using Trash_Can.Controls;
using Trash_Can.Trashs;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    public sealed partial class MainPage : Page
    {


        private string Untitled = "Untitled";
        private string InputText = "Please input the text";


        // FlowDirection
        private void ConstructFlowDirection()
        {
            bool isRightToLeft = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

            base.FlowDirection = isRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        // Strings
        private void ConstructStrings()
        {
            ResourceLoader resource = ResourceLoader.GetForCurrentView();

            this.PCAppTitleTextBlock.Text = resource.GetString("$DisplayName");
            this.PadAppTitleTextBlock.Text = resource.GetString("$DisplayName");

            this.Untitled = resource.GetString("$Untitled");
            this.InputText = resource.GetString("$InputText");

            {
                this.NewTextBlock.Text = resource.GetString("$MainPage_New");
                this.HomeTextBlock.Text = resource.GetString("$MainPage_Home");
                this.AboutTextBlock.Text = resource.GetString("$MainPage_About");
                this.SettingTextBlock.Text = resource.GetString("$MainPage_Setting");

                this.PCSplitToolTip.Content = resource.GetString("$MainPage_Split");
                this.PhoneSplitToolTip.Content = resource.GetString("$MainPage_Split");
            }

            {
                this.CloseToolTip.Content = resource.GetString("$DrawPage_Close");
                this.PinToolTip.Content = resource.GetString("$DrawPage_Pin");
                this.UndoToolTip.Content = resource.GetString("$DrawPage_Undo");
                this.RedoToolTip.Content = resource.GetString("$DrawPage_Redo");
                this.FullScreenToolTip.Content = resource.GetString("$DrawPage_FullScreen");
                this.UnFullScreenToolTip.Content = resource.GetString("$DrawPage_UnFullScreen");

                this.FindToolTip.Content = resource.GetString("$DrawPage_Find");
                this.FindReplaceToolTip.Content = resource.GetString("$DrawPage_Replace");
                this.FindCloseToolTip.Content = resource.GetString("$DrawPage_Close");
                this.NextToolTip.Content = resource.GetString("$DrawPage_Next");
                this.ReplaceButton.Content = resource.GetString("$DrawPage_Replace");
                this.ReplaceAllButton.Content = resource.GetString("$DrawPage_ReplaceAll");

                this.SaveItem.Text = resource.GetString("$DrawPage_Save");
                this.CloseItem.Text = resource.GetString("$DrawPage_Close");
                this.DateItem.Text = resource.GetString("$DrawPage_Date");
                this.FindItem.Text = resource.GetString("$DrawPage_Find");
                this.LocalFolderItem.Text = resource.GetString("$DrawPage_LocalFolder");
                this.RenameItem.Text = resource.GetString("$DrawPage_Rename");
            }

            {
                this.CutToolTip.Content = resource.GetString("Edits_Edit_Cut");
                this.CopyToolTip.Content = resource.GetString("Edits_Edit_Copy");
                this.PasteToolTip.Content = resource.GetString("Edits_Edit_Paste");
                this.SelectAllToolTip.Content = resource.GetString("Edits_Select_All");
                this.ClearToolTip.Content = resource.GetString("Edits_Edit_Clear");

                this.SelectAllItem.Text = resource.GetString("Edits_Select_All");
                this.DeselectItem.Text = resource.GetString("Edits_Select_Deselect");
                this.SelectToStartItem.Text = resource.GetString("Edits_Select_ToStart");
                this.SelectToEndItem.Text = resource.GetString("Edits_Select_ToEnd");
            }

            {
                this.AlignLeftToolTip.Content = resource.GetString("Operates_Horizontally_Left");
                this.AlignCenterToolTip.Content = resource.GetString("Operates_Horizontally_Center");
                this.AlignRightToolTip.Content = resource.GetString("Operates_Horizontally_Right");
            }

            {
                this.TagTypeNoneToolTip.Content = resource.GetString("TagType_None");
                this.TagTypeRedToolTip.Content = resource.GetString("TagType_Red");
                this.TagTypeOrangeToolTip.Content = resource.GetString("TagType_Orange");
                this.TagTypeYellowToolTip.Content = resource.GetString("TagType_Yellow");
                this.TagTypeGreenToolTip.Content = resource.GetString("TagType_Green");
                this.TagTypeBlueToolTip.Content = resource.GetString("TagType_Blue");
                this.TagTypePurpleToolTip.Content = resource.GetString("TagType_Purple");
            }

            {
                this.BoldToolTip.Content = resource.GetString("Texts_FontStyle_Bold");
                this.ItalicToolTip.Content = resource.GetString("Texts_FontStyle_Italic");
                this.UnderlineToolTip.Content = resource.GetString("Texts_FontStyle_Underline");

                this.FontFamilyToolTip.Content = resource.GetString("Texts_FontFamily");
                this.FontSizeToolTip.Content = resource.GetString("Texts_FontSize");

                this.WeightBlackItem.Text = resource.GetString("Texts_FontWeight_Black");
                this.WeightBoldItem.Text = resource.GetString("Texts_FontWeight_Bold");
                this.WeightExtraBlackItem.Text = resource.GetString("Texts_FontWeight_ExtraBlack");
                this.WeightExtraBoldItem.Text = resource.GetString("Texts_FontWeight_ExtraBold");
                this.WeightExtraLightItem.Text = resource.GetString("Texts_FontWeight_ExtraLight");
                this.WeightLightItem.Text = resource.GetString("Texts_FontWeight_Light");
                this.WeightMediumItem.Text = resource.GetString("Texts_FontWeight_Medium");
                this.WeightNormalItem.Text = resource.GetString("Texts_FontWeight_Normal");
                this.WeightSemiBoldItem.Text = resource.GetString("Texts_FontWeight_SemiBold");
                this.WeightSemiLightItem.Text = resource.GetString("Texts_FontWeight_SemiLight");
                this.WeightThinItem.Text = resource.GetString("Texts_FontWeight_Thin");

                this.UnderlineNoneItem.Text = resource.GetString("Texts_Underline_None");
                this.UnderlineNoneItem.Text = resource.GetString("Texts_Underline_None");
                this.UnderlineWordsItem.Text = resource.GetString("Texts_Underline_Words");
                this.UnderlineSingleItem.Text = resource.GetString("Texts_Underline_Single");
                this.UnderlineDoubleItem.Text = resource.GetString("Texts_Underline_Double");
                this.UnderlineDashItem.Text = resource.GetString("Texts_Underline_Dash");
                this.UnderlineDashDotItem.Text = resource.GetString("Texts_Underline_DashDot");
                this.UnderlineDashDotDotItem.Text = resource.GetString("Texts_Underline_DashDotDot");
                this.UnderlineDottedItem.Text = resource.GetString("Texts_Underline_Dotted");
                this.UnderlineLongDashItem.Text = resource.GetString("Texts_Underline_LongDash");
                this.UnderlineThinItem.Text = resource.GetString("Texts_Underline_Thin");
                this.UnderlineThickItem.Text = resource.GetString("Texts_Underline_Thick");
                this.UnderlineThickDashItem.Text = resource.GetString("Texts_Underline_ThickDash");
                this.UnderlineThickDashDotItem.Text = resource.GetString("Texts_Underline_ThickDashDot");
                this.UnderlineThickDashDotDotItem.Text = resource.GetString("Texts_Underline_ThickDashDotDot");
                this.UnderlineThickDottedItem.Text = resource.GetString("Texts_Underline_ThickDotted");
                this.UnderlineThickLongDashItem.Text = resource.GetString("Texts_Underline_ThickLongDash");
                this.UnderlineWaveItem.Text = resource.GetString("Texts_Underline_Wave");
                this.UnderlineDoubleWaveItem.Text = resource.GetString("Texts_Underline_DoubleWave");
                this.UnderlineHeavyWaveItem.Text = resource.GetString("Texts_Underline_HeavyWave");
            }
        }

        private void ConstructHead()
        {
            this.CloseButton.Click += async (s, e) =>
            {
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
            };
            this.PinButton.Click += (s, e) => this.FootPanel.IsShow = !this.FootPanel.IsShow;


            this.UndoButton.Click += (s, e) =>
            {
                if (this.Document.CanUndo() == false) return;
                this.Document.Undo();
                this.Update();
                this.Focus();
            };
            this.RedoButton.Click += (s, e) =>
            {
                if (this.Document.CanRedo() == false) return;
                this.Document.Redo();
                this.Update();
                this.Focus();
            };


            this.FullScreenButton.Click += (s, e) =>
            {
                this._vsIsFullScreen = true;
                this.VisualState = this.VisualState; // VisualState
            };
            this.UnFullScreenButton.Click += (s, e) =>
            {
                this._vsIsFullScreen = false;
                this.VisualState = this.VisualState; // VisualState
            };


            this.TitleButton.Holding += (s, e) => this.TitleFlyout.ShowAt(this.HeadBorder);
            this.TitleButton.RightTapped += (s, e) => this.TitleFlyout.ShowAt(this.HeadBorder);
            this.TitleButton.Click += (s, e) => this.TitleFlyout.ShowAt(this.HeadBorder);


            this.SaveItem.Click += async (s, e) =>
            {
                this.State = LoadingState.Saving;
                {
                    string name = this.Subtitle;
                    bool result = await this.Export(name);
                    if (result == false)
                    {
                        this.State = LoadingState.SaveFailed;
                        await Task.Delay(1000);
                    }
                }
                this.State = LoadingState.None;
            };
            this.CloseItem.Click += async (s, e) =>
            {
                switch (this.FindMode)
                {
                    case FindMode.None:
                        {
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
                        }
                        break;
                    default:
                        {
                            this.FindMode = FindMode.None;
                        }
                        break;
                }
            };
            this.DateItem.Click += (s, e) =>
            {
                // Clipboard
                DataPackage dataPackage = new DataPackage();
                dataPackage.SetText(DateTimeOffset.Now.ToString());
                Clipboard.SetContent(dataPackage);

                this.Selection.Paste(0);
                this.Update();
                this.Focus();
            };
            this.FindItem.Click += (s, e) => this.FindMode = FindMode.Find;
            this.LocalFolderItem.Click += async (s, e) => await this.LaunchFolder(this.Subtitle);
            this.RenameItem.Click += async (s, e) =>
            {
                if (this.FlipView.SelectedItem is TrashItem item)
                {
                    this.Editor.Trash = item.Properties;
                    await this.Editor.ShowAsync(ContentDialogPlacement.InPlace);
                }
            };
        }


        private void Update()
        {
            this.UndoButton.IsEnabled = this.Document.CanUndo();
            this.RedoButton.IsEnabled = this.Document.CanRedo();
        }
        private void Update2()
        {
            this.UndoButton.IsEnabled = false;
            this.RedoButton.IsEnabled = false;
        }
        private void Focus()
        {
            this.RichEditBox.Focus(FocusState.Keyboard);
        }

    }
}