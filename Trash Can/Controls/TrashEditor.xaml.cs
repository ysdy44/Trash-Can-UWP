using System.Collections.Generic;
using System.Linq;
using Trash_Can.Trashs;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can.Controls
{
    public sealed partial class TrashEditor : ContentDialog
    {

        //@Converter
        private string StringToEmptyConverter(string value) => value == null ? string.Empty : value;

        public string Subtitle { get => this.SubtitleTextBlock.Text; set => this.SubtitleTextBlock.Text = value; }

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
                this.TitleTextBox.Text = this.StringToEmptyConverter(value?.Title);
                this.CommentTextBox.Text = this.StringToEmptyConverter(value?.Comment);

                this.KeywordsControl0.TextBox.Text = this.GetKeyword(this.KeywordsControl0.ReorderIndex, value.Keywords);
                this.KeywordsControl1.TextBox.Text = this.GetKeyword(this.KeywordsControl1.ReorderIndex, value.Keywords);
                this.KeywordsControl2.TextBox.Text = this.GetKeyword(this.KeywordsControl2.ReorderIndex, value.Keywords);
                this.KeywordsControl3.TextBox.Text = this.GetKeyword(this.KeywordsControl3.ReorderIndex, value.Keywords);
                this.KeywordsControl4.TextBox.Text = this.GetKeyword(this.KeywordsControl4.ReorderIndex, value.Keywords);
            }
        }

        public TrashEditor()
        {
            this.InitializeComponent();
            this.ConstructStrings();
            this.ConstructGotFocus();
            
            this.StackPanel.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.KeywordsControl0.Width =
                this.KeywordsControl1.Width =
                this.KeywordsControl2.Width =
                this.KeywordsControl3.Width =
                this.KeywordsControl4.Width =
                e.NewSize.Width;
            };

            this.TitleTextBox.GotFocus += (s, e) => this.TitleTextBox.SelectAll();
            this.CommentTextBox.GotFocus += (s, e) => this.CommentTextBox.SelectAll();
            this.KeywordsControl0.TextBox.GotFocus += (s, e) => this.KeywordsControl0.TextBox.SelectAll();
            this.KeywordsControl1.TextBox.GotFocus += (s, e) => this.KeywordsControl1.TextBox.SelectAll();
            this.KeywordsControl2.TextBox.GotFocus += (s, e) => this.KeywordsControl2.TextBox.SelectAll();
            this.KeywordsControl3.TextBox.GotFocus += (s, e) => this.KeywordsControl3.TextBox.SelectAll();
            this.KeywordsControl4.TextBox.GotFocus += (s, e) => this.KeywordsControl4.TextBox.SelectAll();
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

        private void ConstructGotFocus()
        {
            this.TitleTextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.CommentTextBox.Focus(FocusState.Keyboard);
                        break;
                    default:
                        break;
                }
            };
            this.CommentTextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.SetFocus(0);
                        break;
                }
            };
            this.KeywordsControl0.TextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.SetFocus(1);
                        break;
                }
            };
            this.KeywordsControl1.TextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.SetFocus(2);
                        break;
                }
            };
            this.KeywordsControl2.TextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.SetFocus(3);
                        break;
                }
            };
            this.KeywordsControl3.TextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.SetFocus(4);
                        break;
                }
            };
            this.KeywordsControl4.TextBox.KeyDown += (s, e) =>
            {
                switch (e.OriginalKey)
                {
                    case VirtualKey.Enter:
                        this.SetFocus(5);
                        break;
                }
            };
        }
        
        private void SetFocus(int index)
        {
            if (this.KeywordsControl0.ReorderIndex == index) this.KeywordsControl0.TextBox.Focus(FocusState.Keyboard);
            else if (this.KeywordsControl1.ReorderIndex == index) this.KeywordsControl1.TextBox.Focus(FocusState.Keyboard);
            else if (this.KeywordsControl2.ReorderIndex == index) this.KeywordsControl2.TextBox.Focus(FocusState.Keyboard);
            else if (this.KeywordsControl3.ReorderIndex == index) this.KeywordsControl3.TextBox.Focus(FocusState.Keyboard);
            else if (this.KeywordsControl4.ReorderIndex == index) this.KeywordsControl4.TextBox.Focus(FocusState.Keyboard);
            else this.TitleTextBox.Focus(FocusState.Keyboard);
        }

        private string GetKeyword(int index, IList<string> keywords) =>
            keywords == null ?
                string.Empty :
                index < keywords.Count ?
                    this.StringToEmptyConverter(keywords[index]) :
                    string.Empty;

        private IEnumerable<string> GetKeywords()
        {
            for (int i = 0; i < 5; i++)
            {
                string text =
                      (this.KeywordsControl0.ReorderIndex == i) ? this.KeywordsControl0.TextBox.Text :
                      (this.KeywordsControl1.ReorderIndex == i) ? this.KeywordsControl1.TextBox.Text :
                      (this.KeywordsControl2.ReorderIndex == i) ? this.KeywordsControl2.TextBox.Text :
                      (this.KeywordsControl3.ReorderIndex == i) ? this.KeywordsControl3.TextBox.Text :
                      (this.KeywordsControl4.ReorderIndex == i) ? this.KeywordsControl4.TextBox.Text :
                      null;

                if (string.IsNullOrEmpty(text)) continue;

                yield return text;
            }
        }

    }
}