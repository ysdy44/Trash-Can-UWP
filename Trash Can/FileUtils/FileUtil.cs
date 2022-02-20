using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Trash_Can
{
    public static partial class FileUtil
    {

        public static async Task<StorageFile> CreateFileAsync()
        {
            string name = FileUtil.NewName();
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(name);
            return file;
        }
        public static async Task<IStorageItem> GetFile(string name)
        {
            IStorageItem item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(name);
            return item;
        }


        #region  IsExists


        /// <summary>
        /// To know if a file exists.
        /// </summary>
        /// <param name="fileName"> The file name. </param>
        /// <returns> The exists. </returns>
        public static async Task<bool> IsFileExistsInLocalFolder(string fileName) => await FileUtil.IsFileExists(fileName, ApplicationData.Current.LocalFolder);

        /// <summary>
        /// To know if a file exists.
        /// </summary>
        /// <param name="fileName"> The file name. </param>
        /// <returns> The exists. </returns>
        public static async Task<bool> IsFileExistsInTemporaryFolder(string fileName) => await FileUtil.IsFileExists(fileName, ApplicationData.Current.TemporaryFolder);

        /// <summary>
        /// To know if a file exists.
        /// </summary>
        /// <param name="fileName"> The file name. </param>
        /// <param name="folder"> The folder. </param>
        /// <returns> The exists. </returns>
        public static async Task<bool> IsFileExists(string fileName, StorageFolder folder)
        {
            IStorageItem item = await folder.TryGetItemAsync(fileName);
            return (item is null) == false;
        }


        #endregion


    }
}