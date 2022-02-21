using Trash_Can.Blends;
using Trash_Can.Texts;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    public sealed partial class MainPage : Page
    {

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