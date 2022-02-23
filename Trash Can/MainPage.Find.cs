using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    public enum FindMode
    {
        None,
        Find,
        Replace,
    }

    public sealed partial class MainPage : Page
    {


        #region DependencyProperty


        /// <summary> Gets or set the find mode for <see cref="MainPage"/>. </summary>
        public FindMode FindMode
        {
            get => (FindMode)base.GetValue(FindModeProperty);
            set => base.SetValue(FindModeProperty, value);
        }
        /// <summary> Identifies the <see cref = "MainPage.FindMode" /> dependency property. </summary>
        public static readonly DependencyProperty FindModeProperty = DependencyProperty.Register(nameof(FindMode), typeof(FindMode), typeof(MainPage), new PropertyMetadata(FindMode.None, (sender, e) =>
        {
            MainPage control = (MainPage)sender;

            if (e.NewValue is FindMode value)
            {
                Thickness pageMargin = control.SettingDialog.GetPageMargin();
                control.UpdateRichEditBoxPadding(value, pageMargin);

                switch (value)
                {
                    case FindMode.None:
                        control.FindBorder.Visibility = Visibility.Collapsed;
                        control.ReplaceGrid.Visibility = Visibility.Collapsed;
                        break;
                    case FindMode.Find:
                        control.FindBorder.Visibility = Visibility.Visible;
                        control.ReplaceGrid.Visibility = Visibility.Collapsed;
                        if (e.OldValue is FindMode.None)
                        {
                            control.FindTextBox.Focus(FocusState.Keyboard);
                        }

                        control.FindMatches();
                        break;
                    case FindMode.Replace:
                        control.FindBorder.Visibility = Visibility.Visible;
                        control.ReplaceGrid.Visibility = Visibility.Visible;

                        control.FindMatches();
                        break;
                    default:
                        break;
                }
            }
        }));

        private void UpdateRichEditBoxPadding(FindMode mode, Thickness pageMargin)
        {
            switch (mode)
            {
                case FindMode.None: break;
                case FindMode.Find: pageMargin.Top += 54; break;
                case FindMode.Replace: pageMargin.Top += 102; break;
                default: break;
            }
            this.RichEditBox.Padding = pageMargin;
        }


        #endregion


        private void ConstructFind()
        {
            this.FindNextButton.Click += (s, e) =>
            {
                this.Focus();
                this.FindMatches();
            };

            this.FindCloseButton.Click += (s, e) =>
            {
                this.Focus();
                this.FindMode = FindMode.None;
            };
            this.FindReplaceButton.Click += (s, e) =>
            {
                this.Focus();
                switch (this.FindMode)
                {
                    case FindMode.Replace:
                        this.FindMode = FindMode.Find;
                        break;
                    default:
                        this.FindMode = FindMode.Replace;
                        break;
                }
            };

            this.ReplaceButton.Click += (s, e) =>
            {
                this.Focus();
                this.ReplaceMatches();
            };
            this.ReplaceAllButton.Click += (s, e) =>
            {
                this.Focus();
                while (this.ReplaceMatches())
                {
                }
            };
        }


        private bool ReplaceMatches()
        {
            string textToFind = this.FindTextBox.Text;
            if (string.IsNullOrEmpty(textToFind)) return false;
            string textToReplace = this.ReplaceTextBox.Text;

            bool result = this.FindMatchesCore(textToFind, 1);
            if (result) return this.ReplaceMatchesCore(textToReplace);

            this.Selection.SetRange(0, 0);
            bool result2 = this.FindMatchesCore(textToFind, 0);
            if (result2) return this.ReplaceMatchesCore(textToReplace);

            return false;
        }

        private bool FindMatches()
        {
            string textToFind = this.FindTextBox.Text;
            if (string.IsNullOrEmpty(textToFind)) return false;

            bool result = this.FindMatchesCore(textToFind, 1);
            if (result) return true;

            this.Selection.SetRange(0, 0);
            return this.FindMatchesCore(textToFind, 0);
        }

        private bool ReplaceMatchesCore(string textToReplace)
        {
            this.Selection.SetText(TextSetOptions.FormatRtf, textToReplace);
            return true;
        }

        private bool FindMatchesCore(string textToFind, int offset)
        {
            ITextRange searchRange = this.Document.GetRange(this.Selection.StartPosition + offset, int.MaxValue);

            int length = searchRange.FindText(textToFind, TextConstants.MaxUnitCount, FindOptions.None);
            if (length == 0) return false;

            this.Selection.SetRange(searchRange.StartPosition, searchRange.EndPosition);
            return true;
        }


    }
}