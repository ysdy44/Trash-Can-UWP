﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Trash_Can.Trashs;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    public sealed partial class MainPage : Page
    {

        private async Task<bool> LaunchFolder(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            StorageFile file = await FileUtil.FIndRtfFile(name);
            if (file == null) return false;

            IStorageFolder folder = ApplicationData.Current.LocalFolder;
            return await Launcher.LaunchFolderAsync(folder, new FolderLauncherOptions
            {
                ItemsToSelect =
                {
                    file
                }
            });
        }

        private async Task<string> New()
        {
            StorageFile file = await FileUtil.CreateFileAsync();
            TrashItem trash = FileUtil.ConstructTrashItem(file);
            trash.Properties = new Trash
            {
                Title = this.Untitled
            };

            this.Items.Add(trash);
            this.Save();

            this.FlipView.SelectedIndex = 0;

            return file.Name;
        }


        /// <summary>
        /// Open
        /// </summary>
        private async Task<bool> Open(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            foreach (TrashItem item in this.Items)
            {
                if (item.Name == name) this.Title = item.Title;
            }
            this.Subtitle = name;
            this.Update2();

            if (await FileUtil.GetFile(name) is IStorageFile file2)
            {
                using (IRandomAccessStream stream = await file2.OpenAsync(FileAccessMode.ReadWrite))
                {
                    this.Document.LoadFromStream(TextSetOptions.FormatRtf, stream);
                }
                this._vsIsWritable = true;
                this.VisualState = this.VisualState; // VisualState
                return true;
            }

            return false;
        }


        /// <summary>
        /// Export to ...
        /// </summary>
        private async Task<bool> Export(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (await FileUtil.GetFile(name) is IStorageFile file2)
            {
                using (IRandomAccessStream stream = await file2.OpenAsync(FileAccessMode.ReadWrite))
                {
                    this.Document.SaveToStream(TextGetOptions.FormatRtf, stream);
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Save the current project.
        /// </summary>
        private async void Save()
        {
            await XML.SaveTrashsFile(this.Items);
        }


        /// <summary>
        /// Exit the current Project.
        /// </summary>
        private async Task<bool> Exit(string name)
        {
            if (this._vsIsWritable == false) return false;

            if (this._vsIsFullScreen)
            {
                this._vsIsFullScreen = false;
                this.VisualState = this.VisualState; // VisualState
                return false;
            }

            // Exit
            {
                this.Title = string.Empty;
                this.Subtitle = string.Empty;
                this.FindMode = FindMode.None;

                this._vsIsFullScreen = false;
                this._vsIsWritable = false;
                this.VisualState = this.VisualState; // VisualState
            }

            await this.Export(name);

            // Order
            if (this.Items.FirstOrDefault(c => c.Name == name) is TrashItem item)
            {
                item.DateCreated = DateTimeOffset.Now;

                int oldIndex = this.Items.IndexOf(item);
                int newIndex = 0;
                if (oldIndex != newIndex)
                {
                    this.Items.Move(oldIndex, newIndex);
                }

                this.Save();

                return true;
            }

            return false;
        }

    }
}