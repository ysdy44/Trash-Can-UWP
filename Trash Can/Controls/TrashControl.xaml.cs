using System;
using Trash_Can.Trashs;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can.Controls
{
    public sealed partial class TrashControl : UserControl
    {

        //@Delegate  
        /// <summary> Occurs when Operated. </summary>
        public static event EventHandler<OperateType> Operated;


        //@Converter
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanToVisibilityConverter(bool value) => value ? Visibility.Collapsed : Visibility.Visible;
        private bool ReverseBooleanConverter(bool value) => !value;


        #region DependencyProperty


        /// <summary> Gets or set the model for <see cref="TrashControl"/>. </summary>
        public TrashItem Model
        {
            get => (TrashItem)base.GetValue(ModelProperty);
            set => base.SetValue(ModelProperty, value);
        }
        /// <summary> Identifies the <see cref = "TrashControl.Model" /> dependency property. </summary>
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(TrashItem), typeof(TrashControl), new PropertyMetadata(null));


        #endregion

        public TrashControl()
        {
            this.InitializeComponent();
            this.ConstructStrings();

            this.EditButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.Edit); // Delegate
            this.RenameButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.Rename); // Delegate
            this.RemoveButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.Remove); // Delegate
            this.DuplicateButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.Duplicate); // Delegate
            this.FavoriteButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.Favorite); // Delegate
            this.UnFavoriteButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.UnFavorite); // Delegate
            this.LocalFolderButton.Click += (s, e) => TrashControl.Operated?.Invoke(this.Model, OperateType.LocalFolder); // Delegate
        }
    }

    public sealed partial class TrashControl : UserControl
    {

        // Strings
        private void ConstructStrings()
        {
            ResourceLoader resource = ResourceLoader.GetForCurrentView();

            this.EditButton.Content = resource.GetString("$MainPage_Edit");
            this.LocalFolderToolTip.Content = resource.GetString("$MainPage_LocalFolder");
            this.FavoriteToolTip.Content = resource.GetString("$MainPage_Favorite");
            this.UnFavoriteToolTip.Content = resource.GetString("$MainPage_UnFavorite");
            this.RenameToolTip.Content = resource.GetString("$MainPage_Rename");
            this.RemoveToolTip.Content = resource.GetString("$MainPage_Remove");
            this.DuplicateToolTip.Content = resource.GetString("$MainPage_Duplicate");
        }

    }
}