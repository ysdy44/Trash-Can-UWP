using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Trash_Can
{
    public static partial class FileUtil
    {

        public async static Task<StorageFolder> PickFolderAsync(PickerLocationId location)
        {
            // Picker
            FolderPicker openPicker = new FolderPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = location,
            };

            if (openPicker is null) return null;

            // Folder
            StorageFolder folder = await openPicker.PickSingleFolderAsync();
            return folder;
        }

    }
}