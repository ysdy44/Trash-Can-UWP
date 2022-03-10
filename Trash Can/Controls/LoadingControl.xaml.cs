using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can.Controls
{
    /// <summary> 
    /// Represents a dialog used to loading....
    /// </summary>
    public sealed partial class LoadingControl : UserControl
    {

        // String
        readonly static ResourceLoader resource = ResourceLoader.GetForCurrentView();
        private string StringsConverter(LoadingState value)
        {
            if (value == LoadingState.None) return null;

            return LoadingControl.resource.GetString($"$Loading_{value}");
        }

        //@Converter
        private Visibility VisibilityConverter(LoadingState value) => value == LoadingState.None ? Visibility.Collapsed : Visibility.Visible;

        private bool ProgressRingBooleanConverter(LoadingState value) => value == LoadingState.Loading || value == LoadingState.Saving;
        private Visibility ProgressRingVisibilityConverter(LoadingState value) => this.ProgressRingBooleanConverter(value) ? Visibility.Visible : Visibility.Collapsed;

        private Visibility CorrectVisibilityConverter(LoadingState value) => value == LoadingState.SaveSuccess ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ErrorVisibilityConverter(LoadingState value)
        {
            switch (value)
            {
                case LoadingState.LoadFailed:
                case LoadingState.FileCorrupt:
                case LoadingState.FileNull:
                case LoadingState.SaveFailed: return Visibility.Visible;
                default: return Visibility.Collapsed;
            }
        }


        #region DependencyProperty


        /// <summary> Gets or sets whether the <see cref = "LoadingControl" /> state. </summary>
        public LoadingState State
        {
            get => (LoadingState)base.GetValue(StateProperty);
            set => base.SetValue(StateProperty, value);
        }
        /// <summary> Identifies the <see cref = "LoadingControl.State" /> dependency property. </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(LoadingState), typeof(LoadingControl), new PropertyMetadata(LoadingState.None));


        #endregion


        //@Construct
        /// <summary>
        /// Initializes a LoadingDialog. 
        /// </summary>
        public LoadingControl()
        {
            this.InitializeComponent();
        }

    }
}