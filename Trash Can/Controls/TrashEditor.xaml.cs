using System.Collections.Generic;
using System.Linq;
using Trash_Can.Trashs;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Trash_Can.Controls
{
    public sealed partial class TrashEditor : ContentDialog
    {

        public Trash Trash
        {
            get => new Trash
            {
                Title = this.TitleTextBox.Text,
                Comment = this.CommentTextBox.Text,
                Keywords = this.GetKeywords().ToList(),
            };
            set
            {
                if (value == null) return;
                this.TitleTextBox.Text = value.Title == null ? string.Empty : value.Title;
                this.CommentTextBox.Text = value.Comment == null ? string.Empty : value.Comment;
                if (value.Keywords == null) return;
                for (int i = 0; i < value.Keywords.Count; i++)
                {
                    string text2 = value.Keywords[i];
                    string text = text2 == null ? string.Empty : text2;

                    switch (i)
                    {
                        case 0: this.KeywordsControl0.Text = text; break;
                        case 1: this.KeywordsControl1.Text = text; break;
                        case 2: this.KeywordsControl2.Text = text; break;
                        case 3: this.KeywordsControl3.Text = text; break;
                        case 4: this.KeywordsControl4.Text = text; break;
                        default: break;
                    }
                }
            }
        }

        public TrashEditor()
        {
            this.InitializeComponent();
            this.ConstructStrings();

            this.StackPanel.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                double width = e.NewSize.Width;
                this.KeywordsControl0.Width = width;
                this.KeywordsControl1.Width = width;
                this.KeywordsControl2.Width = width;
                this.KeywordsControl3.Width = width;
                this.KeywordsControl4.Width = width;
            };
        }

        private IEnumerable<string> GetKeywords()
        {
            for (int i = 0; i < 5; i++)
            {
                string text =
                      (this.KeywordsControl0.ReorderIndex == i) ? this.KeywordsControl0.Text :
                      (this.KeywordsControl1.ReorderIndex == i) ? this.KeywordsControl1.Text :
                      (this.KeywordsControl2.ReorderIndex == i) ? this.KeywordsControl2.Text :
                      (this.KeywordsControl3.ReorderIndex == i) ? this.KeywordsControl3.Text :
                      (this.KeywordsControl4.ReorderIndex == i) ? this.KeywordsControl4.Text :
                      null;

                if (string.IsNullOrEmpty(text)) continue;

                yield return text;
            }
        }

    }

    public sealed partial class TrashEditor : ContentDialog
    {

        private string InputText = "Please input the text";

        // Strings
        private void ConstructStrings()
        {
            ResourceLoader resource = ResourceLoader.GetForCurrentView();

            this.InputText = resource.GetString("$InputText");

            base.SecondaryButtonText = resource.GetString("$DrawPage_Editor_Close");
            base.PrimaryButtonText = resource.GetString("$DrawPage_Editor_Primary");

            this.TitleTextBox.PlaceholderText = this.InputText;
            this.CommentTextBox.PlaceholderText = this.InputText;

            this.TitleTextBlock.Text = resource.GetString("$DrawPage_Editor_Rename");
            this.TitleTextBox.Header = resource.GetString("$DrawPage_Editor_Title");
            this.CommentTextBox.Header = resource.GetString("$DrawPage_Editor_Comment");
            this.KeywordsTextBlock.Text = resource.GetString("$DrawPage_Editor_Keywords");
        }

    }
}