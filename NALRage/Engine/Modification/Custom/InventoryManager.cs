using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NALRage.Engine.Modification.Custom.Inventory;

namespace NALRage.Engine.Modification.Custom
{
    internal static class InventoryManager
    {
        private static readonly List<Type> items = new List<Type>();
        private static PlayerInventory currentInventory;
        
        internal static void LoadInventory(Item[] items)
        {

        }
    }
}
