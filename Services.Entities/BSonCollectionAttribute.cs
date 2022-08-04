using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BSonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }


        public BSonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
