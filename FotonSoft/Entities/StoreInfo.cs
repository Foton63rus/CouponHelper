using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotonSoft.Entities
{
    public class StoreInfo
    {
        public string Name { get; private set; }
        public string WebLink { get; private set; }

        public StoreInfo( string name, string weblink)
        {
            Name = Name;
            WebLink = weblink;
        }

        public override string ToString()
        {
            return String.Format("[Store: \"{0}\" {1}]", Name, WebLink);
        }
    }
}
