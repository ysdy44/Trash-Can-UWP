using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can.Elements
{
    /// <summary>
    /// Item of <see cref="AccentDecorator"/>.
    /// </summary>
    public sealed partial class AccentDecoratorItem : UserControl
    {

        //@Converter
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;


        #region DependencyProperty


        /// <summary> Gets or sets the IsFavorite of<see cref = "AccentDecoratorItem" />. </summary>
        public bool IsFavorite
        {
            get => (bool)base.GetValue(IsFavoriteProperty);
            set => base.SetValue(IsFavoriteProperty, value);
        }
        /// <summary> Identifies the <see cref = "AccentDecoratorItem.IsFavorite" /> dependency property. </summary>
        public static readonly DependencyProperty IsFavoriteProperty = DependencyProperty.Register(nameof(IsFavorite), typeof(bool), typeof(AccentDecoratorItem), new PropertyMetadata(false));


        #endregion


        //@Construct
        /// <summary>
        /// Initializes a AccentDecoratorItem. 
        /// </summary> 
        public AccentDecoratorItem()
        {
            this.InitializeComponent();
        }

    }
}