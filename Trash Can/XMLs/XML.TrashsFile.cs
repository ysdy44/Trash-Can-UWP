using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trash_Can.Trashs;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Trash_Can
{
    public static partial class XML
    {

        public static async Task<IEnumerable<TrashItem>> ConstructTrashsFile()
        {
            StorageFile file = null;
            bool isLocalFilterExists = await FileUtil.IsFileExistsInLocalFolder("Trashs.xml");

            if (isLocalFilterExists)
            {
                // Read the file from the local folder.
                file = await ApplicationData.Current.LocalFolder.GetFileAsync("Trashs.xml");
            }
            else
            {
                return null;
            }

            if (file is null) return null;

            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                try
                {
                    XDocument document = XDocument.Load(stream);

                    IEnumerable<TrashItem> source = XML.LoadTrashs(document);
                    return source;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static async Task SaveTrashsFile(IEnumerable<TrashItem> Trashs)
        {
            XDocument document = XML.SaveTrashs(Trashs);

            // Save the xml file.      
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Trashs.xml", CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (Stream stream = fileStream.AsStream())
                {
                    document.Save(stream);
                }
            }
        }

    }
}