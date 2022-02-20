using System.Collections.Generic;
using System.Linq;

namespace Trash_Can.Trashs
{
    public sealed class Trash
    {

        public string Title { get; set; }
        public string Comment { get; set; }
        public IList<string> Keywords { get; set; }

        public Trash Clone()
        {
            return new Trash
            {
                Title = this.Title,
                Comment = this.Comment,
                Keywords = this.Keywords.ToList(),
            };
        }

    }
}