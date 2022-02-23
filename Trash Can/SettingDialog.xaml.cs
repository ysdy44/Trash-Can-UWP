using System;
using System.Collections.Generic;
using Trash_Can.Texts;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    /// <summary> 
    /// Represents a dialog used to setting....
    /// </summary>
    public sealed partial class SettingDialog : ContentDialog
    {

        //@Delegate  
        /// <summary> Occurs when Setted. </summary>
        public event EventHandler<string> FontFamilySetted;
        /// <summary> Occurs when Setted. </summary>
        public event EventHandler<float> FontSizeSetted;
        /// <summary> Occurs when Setted. </summary>
        public event EventHandler<Thickness> PageMarginSetted;

        readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        private readonly IList<Thickness> PageMargins = new List<Thickness>
        {
            new Thickness(0),
            new Thickness(2),
            new Thickness(4),
            new Thickness(8,6,8,6),
            new Thickness(16,12,16,12),
            new Thickness(18,14,18,14),
            new Thickness(24,16,24,16),
            new Thickness(32,28,32,28),
        };

        //@Construct
        /// <summary>
        /// Initializes a SettingDialog. 
        /// </summary>
        public SettingDialog()
        {
            this.InitializeComponent();
            this.ConstructTheme();
            this.ConstructStrings();
            this.ConstructFontFamily();
            this.ConstructFontSize();
            this.ConstructPageMargin();

            base.SecondaryButtonClick += (s, e) => base.Hide();
            base.PrimaryButtonClick += (s, e) => base.Hide();
        }

        public void SetTheme(ElementTheme value)
        {
            this.LocalSettings.Values["Theme"] = (int)value;
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                if (frameworkElement.RequestedTheme == value) return;
                frameworkElement.RequestedTheme = value;
            }
        }
        public ElementTheme GetTheme()
        {
            if (this.LocalSettings.Values.ContainsKey("Theme"))
            {
                if (this.LocalSettings.Values["Theme"] is int item)
                {
                    return (ElementTheme)item;
                }
            }
            return base.RequestedTheme;
        }

        public void SetFontFamily(string value) => this.LocalSettings.Values["FontFamily"] = value;
        public string GetFontFamily()
        {
            if (this.LocalSettings.Values.ContainsKey("FontFamily"))
            {
                if (this.LocalSettings.Values["FontFamily"] is string item)
                {
                    return item;
                }
            }
            return base.FontFamily.Source;
        }

        public void SetFontSize(float value) => this.LocalSettings.Values["FontSize"] = value;
        public float GetFontSize()
        {
            if (this.LocalSettings.Values.ContainsKey("FontSize"))
            {
                if (this.LocalSettings.Values["FontSize"] is float item)
                {
                    return item;
                }
            }
            return (float)base.FontSize;
        }

        public void SetPageMargin(Thickness value) => this.LocalSettings.Values["PageMargin"] = new double[] { value.Left, value.Top, value.Right, value.Bottom };
        public Thickness GetPageMargin()
        {
            if (this.LocalSettings.Values.ContainsKey("PageMargin"))
            {
                if (this.LocalSettings.Values["PageMargin"] is double[] item)
                {
                    return new Thickness(item[0], item[1], item[2], item[3]);
                }
            }
            return new Thickness(18, 14, 18, 14);
        }

    }


    public sealed partial class SettingDialog : ContentDialog
    {
        private void ConstructStrings()
        {
            ResourceLoader resource = ResourceLoader.GetForCurrentView();

            base.SecondaryButtonText = resource.GetString("$SettingPage_Close");
            base.PrimaryButtonText = resource.GetString("$SettingPage_Primary");

            this.TitleTextBlock.Text = resource.GetString("$MainPage_Setting");
            this.ThemeTextBlock.Text = resource.GetString("$SettingPage_Theme");
            this.LightRadioButton.Content = resource.GetString("$SettingPage_Theme_Light");
            this.DarkRadioButton.Content = resource.GetString("$SettingPage_Theme_Dark");
            this.DefaultRadioButton.Content = resource.GetString("$SettingPage_Theme_UseSystem");

            this.FontFamilyTextBlock.Text = resource.GetString("$SettingPage_Texts_FontFamily");
            this.FontSizeTextBlock.Text = resource.GetString("$SettingPage_Texts_FontSize");
        }

        private void ConstructTheme()
        {
            ElementTheme theme = this.GetTheme();
            this.SetTheme(theme);

            this.LightRadioButton.IsChecked = (theme == ElementTheme.Light);
            this.DarkRadioButton.IsChecked = (theme == ElementTheme.Dark);
            this.DefaultRadioButton.IsChecked = (theme == ElementTheme.Default);

            this.LightRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Light);
            this.DarkRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Dark);
            this.DefaultRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Default);
        }

        private void ConstructFontFamily()
        {
            this.FontFamilyComboBox.ItemsSource = TextsExtensions.FontFamilies;
            this.FontFamilyComboBox.SelectedItem = this.GetFontFamily();
            this.FontFamilyComboBox.SelectionChanged += (s, e) =>
            {
                if (this.FontFamilyComboBox.SelectedItem is string item)
                {
                    this.SetFontFamily(item);
                    this.FontFamilySetted?.Invoke(this, item); // Delegate
                }
            };
        }

        private void ConstructFontSize()
        {
            this.FontSizeComboBox.ItemsSource = TextsExtensions.FontSizes;
            this.FontSizeComboBox.SelectedItem = this.GetFontSize();
            this.FontSizeComboBox.SelectionChanged += (s, e) =>
            {
                if (this.FontSizeComboBox.SelectedItem is float item)
                {
                    this.SetFontSize(item);
                    this.FontSizeSetted?.Invoke(this, item); // Delegate
                }
            };
        }

        private void ConstructPageMargin()
        {
            this.PageMarginComboBox.ItemsSource = this.PageMargins;
            this.PageMarginComboBox.SelectedItem = this.GetPageMargin();
            this.PageMarginComboBox.SelectionChanged += (s, e) =>
            {
                if (this.PageMarginComboBox.SelectedItem is Thickness item)
                {
                    this.SetPageMargin(item);
                    this.PageMarginSetted?.Invoke(this, item); // Delegate
                }
            };
        }

    }
}