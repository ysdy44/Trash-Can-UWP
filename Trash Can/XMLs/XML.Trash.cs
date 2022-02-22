using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Trash_Can.Trashs;
using Windows.UI.Xaml;

namespace Trash_Can
{
    public static partial class XML
    {

        public static XElement SaveTrash(string elementName, TrashItem trash)
        {
            XElement element = new XElement(elementName);

            // SaveWith
            {
                if (trash.Name != null) element.Add(new XAttribute("Name", trash.Name));
                element.Add(new XAttribute("DateCreated", trash.DateCreated.Ticks));
                element.Add(new XAttribute("Theme", (int)trash.Theme));
                element.Add(new XAttribute("IsFavorite", trash.IsFavorite));
            }

            if (trash.Properties != null)
            {
                if (trash.Properties.Title != null) element.Add(new XAttribute("Title", trash.Title));
                if (trash.Properties.Comment != null) element.Add(new XAttribute("Comment", trash.Comment));
                if (trash.Keywords != null) element.Add(new XElement
                (
                    "Keywords",
                    from keyword
                    in trash.Properties.Keywords
                    select new XElement("Keyword", keyword)
                ));
            }

            return element;
        }

        public static TrashItem LoadTrash(XElement element)
        {
            Trash trash = new Trash();
            if (element.Attribute("Title") is XAttribute title) trash.Title = title.Value;
            if (element.Attribute("Comment") is XAttribute comment) trash.Comment = comment.Value;
            if (element.Element("Keywords") is XElement keywords)
            {
                if (keywords.Elements("Keyword") is IEnumerable<XElement> keywords2)
                {
                    trash.Keywords = (
                        from keyword
                        in keywords2
                        select keyword.Value).ToList();
                }
            }

            TrashItem trash2 = new TrashItem
            {
                Properties = trash
            };
            if (element.Attribute("Name") is XAttribute name) trash2.Name = name.Value;
            if (element.Attribute("DateCreated") is XAttribute dateCreated) trash2.DateCreated = new DateTimeOffset((long)dateCreated, TimeSpan.Zero);
            if (element.Attribute("Theme") is XAttribute theme) trash2.Theme = (ElementTheme)(int)theme;
            if (element.Attribute("IsFavorite") is XAttribute isFavorite) trash2.IsFavorite = (bool)isFavorite;

            return trash2;
        }

    }
}