using System;
using System.Linq;
using System.Threading.Tasks;
using Trash_Can.Trashs;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Text;
using Windows.UI.Xaml;
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

        private async Task<TrashItem> New(string title)
        {
            StorageFile file = await FileUtil.CreateFileAsync();
            TrashItem trash = FileUtil.ConstructTrashItem(file, base.ActualTheme);
            trash.Properties = new Trash
            {
                Title = title
            };

            this.Items.Add(trash);
            this.Save();
            return trash;
        }


        private async Task<bool> Activated(IStorageItem tem)
        {
            if (tem == null) return false;
            if (tem is StorageFile file)
            {
                switch (file.FileType.ToLower())
                {
                    case ".rtf":
                        try
                        {
                            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                            {
                                this.Document.LoadFromStream(TextSetOptions.FormatRtf, stream);
                            }
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        break;
                    default:
                        try
                        {
                            string text = await FileIO.ReadTextAsync(file);
                            this.Document.SetText(TextSetOptions.None, text);
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        break;
                }

                this.Title = file.DisplayName;
                this.Subtitle = string.Empty;
                this.Update2();

                this._vsIsWritable = true;
                this.VisualState = this.VisualState; // VisualState
                return true;
            }
            return false;
        }


        /// <summary>
        /// Open
        /// </summary>
        private async Task<bool> Open(TrashItem trash, ElementTheme theme)
        {
            if (trash == null) return false;
            if (string.IsNullOrEmpty(trash.Name)) return false;

            if (await FileUtil.GetFile(trash.Name) is IStorageFile file)
            {
                // Color:
                // Foregournd follow Theme
                if (theme != trash.Theme)
                {
                    try
                    {
                        string text = FileUtil.ReadTextAsync(await FileIO.ReadTextAsync(file), theme);
                        //await FileIO.WriteTextAsync(file, text);
                        this.Document.SetText(TextSetOptions.FormatRtf, text);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                        {
                            this.Document.LoadFromStream(TextSetOptions.FormatRtf, stream);
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                this.Title = trash.Title;
                this.Subtitle = trash.Name;
                this.Update2();

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
            if (await FileUtil.GetFile(name) is IStorageFile file)
            {
                this.Subtitle = name;
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
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


        private bool Close()
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
                return true;
            }
        }


        /// <summary>
        /// Exit the current Project.
        /// </summary>
        private async Task<bool> Exit(TrashItem item, ElementTheme theme)
        {
            bool result = this.Close();
            if (result == false) return false;
            if (this.HasChanged == false) return true;

            await this.Export(item.Name);

            // Order
            {
                item.DateCreated = DateTimeOffset.Now;
                item.Theme = theme;

                int oldIndex = this.Items.IndexOf(item);
                int newIndex = 0;
                if (oldIndex != newIndex)
                {
                    this.Items.Move(oldIndex, newIndex);
                }

                this.Save();

                return true;
            }
        }

    }
}