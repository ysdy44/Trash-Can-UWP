using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Trash_Can.Trashs;

namespace Trash_Can
{
    public static partial class XML
    {

        public static XDocument SaveTrashs(IEnumerable<TrashItem> trashs)
        {
            return new XDocument
            (
                // Set the document definition for xml.
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement
                (
                    "Root",
                    from trash
                    in trashs
                    select XML.SaveTrash("Trash", trash)
                )
            );
        }

        public static IEnumerable<TrashItem> LoadTrashs(XDocument document)
        {
            if (document.Element("Root") is XElement root)
            {
                if (root.Elements("Trash") is IEnumerable<XElement> trashs)
                {
                    return
                        from trash
                        in trashs
                        select XML.LoadTrash(trash);
                }
            }

            return null;
        }

    }
}