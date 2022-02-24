using System;
using Trash_Can.Elements;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Trash_Can.Controls
{
    public sealed partial class ReorderControl : Reorder
    {

        //@Content
        public TextBox TextBox => this._TextBox;

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