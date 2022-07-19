using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Trash_Can
{
    public static partial class FileUtil
    {

        /// <summary>
        /// The file picker is displayed so that the user can select a file.
        /// </summary>
        /// <param name="location"> The destination locationId. </param>
        /// <returns> The product file. </returns>
        public async static Task<StorageFile> PickSingleImageFileAsync(PickerLocationId location)
        {
            // Picker
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = location,
                FileTypeFilter =
                {
                     ".jpg", ".jpeg", ".jpe",
                     ".png",
                     ".gif",
                     ".bmp", ".dib",
                     ".tif", ".tiff",
                }
            };

            // File
            StorageFile file = await openPicker.PickSingleFileAsync();
            return file;
        }

    }
}