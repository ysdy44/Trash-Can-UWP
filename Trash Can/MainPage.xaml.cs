using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Trash_Can.Controls;
using Trash_Can.Elements;
using Trash_Can.Trashs;
using Windows.ApplicationModel.Core;
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
        private Symbol VisibilityToPinConverter(Visibility value) => value == Visibility.Visible ? Symbol.UnPin : Symbol.Pin;


        public string Title { get => this.TitleTextBlock.Text; set => this.TitleTextBlock.Text = value; }
        public string Subtitle { get => this.SubtitleTextBlock.Text; set => this.SubtitleTextBlock.Text = value; }
        public ITextDocument Document => this.RichEditBox.Document;
        public ITextRange Selection => this.Document.Selection;
        public ITextCharacterFormat CharacterFormat { get => this.Selection.CharacterFormat; set => this.Selection.CharacterFormat = value; }


        public readonly ObservableCollection<TrashItem> Items = new ObservableCollection<TrashItem>();

        public MainPage()
        {
            this.InitializeComponent();
        }

    }
}