using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trash_Can.Trashs;
using Windows.Storage;

namespace Trash_Can
{
    public static partial class FileUtil
    {

        public static string NewName() => $"{Guid.NewGuid()}.rtf";
      
        public static TrashItem ConstructTrashItem(StorageFile item)
        {

            return new TrashItem
            {
                Name = item.Name,
                DateCreated = item.DateCreated,
            };
        }
     
        public static async Task<TrashItem> CopyRtfFile(string name)
        {
            StorageFile file = await FileUtil.FIndRtfFile(name);
            if (file == null) return null;

            var copy = await file.CopyAsync(ApplicationData.Current.LocalFolder, FileUtil.NewName());
            return FileUtil.ConstructTrashItem(copy);
        }


        public static async Task<IEnumerable<StorageFile>> FIndAllRtfFiles()
        {
            // Get all files.
            IReadOnlyList<StorageFile> files = await ApplicationData.Current.LocalFolder.GetFilesAsync();

            // Sort by Time
            IOrderedEnumerable<StorageFile> orderedFiles = files.OrderByDescending(file => file.DateCreated);

            // Ordered
            IEnumerable<StorageFile> zipFiles =
                from zipFile
                in orderedFiles
                where zipFile.FileType == ".rtf"
                select zipFile;

            return zipFiles;
        }
       
        public static async Task<StorageFile> FIndRtfFile(string name)
        {
            // Get file.
            IStorageItem file = await ApplicationData.Current.LocalFolder.TryGetItemAsync(name);
            if (file is StorageFile item)
            {
                return item;
            }
            return null;
        }

    }
}