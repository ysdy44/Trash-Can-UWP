using System;
using System.Threading.Tasks;
using Trash_Can.Blends;
using Trash_Can.Controls;
using Trash_Can.Texts;
using Trash_Can.Trashs;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    public enum FindMode
    {
        None,
        Find,
        Relace,
    }

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
                    bool result = await this.Export(name);
                    await this.Exit(name);
                    if (result == false)
                    {
                        this.State = LoadingState.SaveFailed;
                        await Task.Delay(1000);
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


            this.TitleButton.Click += (s, e) => this.TitleFlyout.ShowAt(this.TitleButton);
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
                                bool result = await this.Export(name);
                                await this.Exit(name);
                                if (result == false)
                                {
                                    this.State = LoadingState.SaveFailed;
                                    await Task.Delay(1000);
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


        private void ConstructEdit()
        {

            this.CutButton.Click += (s, e) =>
            {
                if (this.Document.CanCopy() == false) return;
                this.Selection.Cut();
                this.Update();
                this.Focus();
            };
            this.CopyButton.Click += (s, e) =>
            {
                if (this.Document.CanCopy() == false) return;
                this.Selection.Copy();
                this.Update();
                this.Focus();
            };
            this.PasteButton.Click += (s, e) =>
            {
                if (this.Document.CanPaste() == false) return;
                this.Selection.Paste(0);
                this.Update();
                this.Focus();
            };
            this.SelectAllButton.Holding += (s, e) => this.SelectAllFlyout.ShowAt(this.SelectAllButton);
            this.SelectAllButton.RightTapped += (s, e) => this.SelectAllFlyout.ShowAt(this.SelectAllButton);
            this.SelectAllButton.Click += (s, e) =>
            {
                this.Selection.SetRange(int.MinValue, int.MaxValue);
                this.Update();
                this.Focus();
            };
            this.DeselectItem.Click += (s, e) =>
            {
                this.Selection.SetRange(0, 0);
                this.Update();
                this.Focus();
            };
            this.SelectAllItem.Click += (s, e) =>
            {
                this.Selection.SetRange(int.MinValue, int.MaxValue);
                this.Update();
                this.Focus();
            };
            this.SelectToStartItem.Click += (s, e) =>
            {
                this.Selection.SetRange(int.MinValue, this.Selection.EndPosition);
                this.Update();
                this.Focus();
            };
            this.SelectToEndItem.Click += (s, e) =>
            {
                this.Selection.SetRange(this.Selection.StartPosition, int.MaxValue);
                this.Update();
                this.Focus();
            };
            this.DeleteButton.Click += (s, e) =>
            {
                if (this.Document.CanCopy() == false) return;
                this.Selection.Delete(TextRangeUnit.Character, 0);
                this.Update();
                this.Focus();
            };
            this.FindButton.Click += (s, e) =>
            {
                switch (this.FindMode)
                {
                    case FindMode.None:
                        this.FindMode = FindMode.Find;
                        break;
                    default:
                        this.FindMode = FindMode.None;
                        break;
                }
            };
        }


        private void ConstructFont()
        {
            this.BoldButton.Holding += (s, e) => this.WeightFlyout.ShowAt(this.BoldButton);
            this.BoldButton.RightTapped += (s, e) => this.WeightFlyout.ShowAt(this.BoldButton);
            this.BoldButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.Bold = FormatEffect.Toggle;
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };

            this.WeightBlackItem.Click += (s, e) => this.Weight(FontWeight2.Black);
            this.WeightBoldItem.Click += (s, e) => this.Weight(FontWeight2.Bold);
            this.WeightExtraBlackItem.Click += (s, e) => this.Weight(FontWeight2.ExtraBlack);
            this.WeightExtraBoldItem.Click += (s, e) => this.Weight(FontWeight2.ExtraBold);
            this.WeightExtraLightItem.Click += (s, e) => this.Weight(FontWeight2.ExtraLight);
            this.WeightLightItem.Click += (s, e) => this.Weight(FontWeight2.Light);
            this.WeightMediumItem.Click += (s, e) => this.Weight(FontWeight2.Medium);
            this.WeightNormalItem.Click += (s, e) => this.Weight(FontWeight2.Normal);
            this.WeightSemiBoldItem.Click += (s, e) => this.Weight(FontWeight2.SemiBold);
            this.WeightSemiLightItem.Click += (s, e) => this.Weight(FontWeight2.SemiLight);
            this.WeightThinItem.Click += (s, e) => this.Weight(FontWeight2.Thin);

            this.ItalicButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.Italic = FormatEffect.Toggle;
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };

            this.UnderlineButton.Holding += (s, e) => this.UnderlineFlyout.ShowAt(this.UnderlineButton);
            this.UnderlineButton.RightTapped += (s, e) => this.UnderlineFlyout.ShowAt(this.UnderlineButton);
            this.UnderlineButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                switch (format.Underline)
                {
                    case UnderlineType.Single:
                        format.Underline = UnderlineType.None;
                        break;
                    default:
                        format.Underline = UnderlineType.Single;
                        break;
                }
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };

            this.UnderlineNoneItem.Click += (s, e) => this.Underline(UnderlineType.None);
            this.UnderlineWordsItem.Click += (s, e) => this.Underline(UnderlineType.Words);
            this.UnderlineSingleItem.Click += (s, e) => this.Underline(UnderlineType.Single);
            this.UnderlineDoubleItem.Click += (s, e) => this.Underline(UnderlineType.Double);
            this.UnderlineDashItem.Click += (s, e) => this.Underline(UnderlineType.Dash);
            this.UnderlineDashDotItem.Click += (s, e) => this.Underline(UnderlineType.DashDot);
            this.UnderlineDashDotDotItem.Click += (s, e) => this.Underline(UnderlineType.DashDotDot);
            this.UnderlineDottedItem.Click += (s, e) => this.Underline(UnderlineType.Dotted);
            this.UnderlineLongDashItem.Click += (s, e) => this.Underline(UnderlineType.LongDash);
            this.UnderlineThinItem.Click += (s, e) => this.Underline(UnderlineType.Thin);
            this.UnderlineThickItem.Click += (s, e) => this.Underline(UnderlineType.Thick);
            this.UnderlineThickDashItem.Click += (s, e) => this.Underline(UnderlineType.ThickDash);
            this.UnderlineThickDashDotItem.Click += (s, e) => this.Underline(UnderlineType.ThickDashDot);
            this.UnderlineThickDashDotDotItem.Click += (s, e) => this.Underline(UnderlineType.ThickDashDotDot);
            this.UnderlineThickDottedItem.Click += (s, e) => this.Underline(UnderlineType.ThickDotted);
            this.UnderlineThickLongDashItem.Click += (s, e) => this.Underline(UnderlineType.ThickLongDash);
            this.UnderlineWaveItem.Click += (s, e) => this.Underline(UnderlineType.Wave);
            this.UnderlineDoubleWaveItem.Click += (s, e) => this.Underline(UnderlineType.DoubleWave);
            this.UnderlineHeavyWaveItem.Click += (s, e) => this.Underline(UnderlineType.HeavyWave);

            this.AlignLeftButton.Click += (s, e) =>
            {
                this.RichEditBox.TextAlignment = TextAlignment.Left;
                this.Update();
                this.Focus();
            };
            this.AlignCenterButton.Click += (s, e) =>
            {
                this.RichEditBox.TextAlignment = TextAlignment.Center;
                this.Update();
                this.Focus();
            };
            this.AlignRightButton.Click += (s, e) =>
            {
                this.RichEditBox.TextAlignment = TextAlignment.Right;
                this.Update();
                this.Focus();
            };
        }


        private void ConstructFontFamily()
        {
            this.FontFamilyComboBox.ItemsSource = TextsExtensions.FontFamilies;
            this.FontFamilyComboBox.SelectedItem = this.RichEditBox.FontFamily.Source;
            this.FontFamilyComboBox.SelectionChanged += (s, e) =>
            {
                if (this.FontFamilyComboBox.SelectedItem is string item)
                {
                    var format = this.CharacterFormat;
                    format.Name = item;
                    this.Selection.CharacterFormat = format;
                    this.Update();
                    this.Focus();
                }
            };
        }


        private void ConstructFontSize()
        {
            this.FontSizeComboBox.ItemsSource = TextsExtensions.FontSizes;
            this.FontSizeComboBox.SelectedItem = (float)this.RichEditBox.FontSize;
            this.FontSizeComboBox.SelectionChanged += (s, e) =>
            {
                if (this.FontSizeComboBox.SelectedItem is float item)
                {
                    var format = this.CharacterFormat;
                    format.Size = item;
                    this.Selection.CharacterFormat = format;
                    this.Update();
                    this.Focus();
                }
            };
        }


        private void ConstructTagType()
        {
            this.TagTypeNoneButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                switch (base.ActualTheme)
                {
                    case ElementTheme.Light:
                        format.ForegroundColor = Colors.Black;
                        break;
                    case ElementTheme.Dark:
                        format.ForegroundColor = Colors.White;
                        break;
                }
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
            this.TagTypeRedButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.ForegroundColor = TagType.Red.ToColor();
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
            this.TagTypeOrangeButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.ForegroundColor = TagType.Orange.ToColor();
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
            this.TagTypeYellowButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.ForegroundColor = TagType.Yellow.ToColor();
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
            this.TagTypeGreenButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.ForegroundColor = TagType.Green.ToColor();
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
            this.TagTypeBlueButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.ForegroundColor = TagType.Blue.ToColor();
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
            this.TagTypePurpleButton.Click += (s, e) =>
            {
                var format = this.CharacterFormat;
                format.ForegroundColor = TagType.Purple.ToColor();
                this.Selection.CharacterFormat = format;
                this.Update();
                this.Focus();
            };
        }


        private void ConstructFind()
        {
            //     this.FindTextBox.TextChanged += (s, e) => this.FindBoxHighlightMatches();
            this.FindNextButton.Click += (s, e) =>
            {
                this.Focus();
                this.FindMatches();
            };

            this.FindCloseButton.Click += (s, e) => this.FindMode = FindMode.None;
            this.FindReplaceButton.Click += (s, e) =>
            {
                switch (this.FindMode)
                {
                    case FindMode.Relace:
                        this.FindMode = FindMode.Find;
                        break;
                    default:
                        this.FindMode = FindMode.Relace;
                        break;
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



        private void FindMatches()
        {
            string textToFind = this.FindTextBox.Text;
            if (string.IsNullOrEmpty(textToFind)) return;

            this.FindMatchesCore(textToFind);
        }
        private void FindMatchesCore(string textToFind)
        {
            ITextRange searchRange = this.Document.GetRange(this.Selection.StartPosition + 1, int.MaxValue);

            int length = searchRange.FindText(textToFind, TextConstants.MaxUnitCount, FindOptions.None);
            if (length != 0)
            {
                this.Selection.SetRange(searchRange.StartPosition, searchRange.EndPosition);
                return;
            }

            this.Selection.SetRange(0, 0);
            this.FindMatchesCore(textToFind);
        }



        private void Underline(UnderlineType type)
        {
            var format = this.CharacterFormat;
            format.Underline = type;
            this.Selection.CharacterFormat = format;
            this.Update();
            this.Focus();
        }
        private void Weight(FontWeight2 weight)
        {
            var format = this.CharacterFormat;
            format.Weight = weight.ToFontWeight().Weight;
            this.Selection.CharacterFormat = format;
            this.Update();
            this.Focus();
        }

    }
}