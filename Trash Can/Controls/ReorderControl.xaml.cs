using Trash_Can.Elements;
using Windows.UI.Xaml.Input;

namespace Trash_Can.Controls
{
    public sealed partial class ReorderControl : Reorder
    {
        public string Text
        {
            get => this.TextBox.Text;
            set => this.TextBox.Text = value;
        }

        public ReorderControl()
        {
            this.InitializeComponent();
            this.Thumb.ManipulationMode = ManipulationModes.TranslateY;
            this.Thumb.ManipulationStarted += base.ThumbManipulationStarted;
            this.Thumb.ManipulationDelta += base.ThumbManipulationDelta;
            this.Thumb.ManipulationCompleted += base.ThumbManipulationCompleted;
        }
    }
}