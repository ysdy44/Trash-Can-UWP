using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Trash_Can.Trashs
{
    public sealed class TrashItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public ElementTheme Theme { get; set; }

        public bool IsFavorite
        {
            get => this.isFavorite;
            set
            {
                this.isFavorite = value;
                this.OnPropertyChanged(nameof(IsFavorite)); // Notify 
            }
        }
        private bool isFavorite;


        public Trash Properties
        {
            get => this.properties;
            set
            {
                this.properties = value;
                this.OnPropertyChanged(nameof(Properties)); // Notify 
                this.OnPropertyChanged(nameof(Title)); // Notify 
                this.OnPropertyChanged(nameof(Comment)); // Notify 
                this.OnPropertyChanged(nameof(Keywords)); // Notify 
            }
        }
        private Trash properties;

        public string Title => this.Properties.Title;
        public string Comment => this.Properties.Comment;
        public IList<string> Keywords => this.Properties.Keywords;



        //@Notify 
        /// <summary> Multicast event for property change notifications. </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName"> Name of the property used to notify listeners. </param>
        protected void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}