// NALRage
// Copyright (C) RelaperCrystal 2020-2021.

using System.Windows.Forms;
using LemonUI.Menus;
using NALRage.Entities;
using Rage;

namespace NALRage.Engine.Menus
{
    internal static class InventoryMenuManager
    {
        internal static PlayerInventory Inventory;

        private static NativeMenu inventoryMenu;

        internal static void Fiber()
        {
            Inventory = new PlayerInventory();

            inventoryMenu = new NativeMenu("Inventory", "PLAYER INVENTORY");
            MenuManager.Pool.Add(inventoryMenu);

            while (Common.InstanceRunning)
            {
                GameFiber.Yield();

                if (Game.IsKeyDown(Keys.I))
                {
                    if (Inventory.Items.Count == 0)
                    {
                        Game.DisplayHelp("You don't have anything in the inventory.");
                        continue;
                    }

                    inventoryMenu.Visible = !inventoryMenu.Visible;
                }
            }
        }

        internal static void Refersh()
        {
            inventoryMenu.Clear();

            foreach (var item in Inventory.Items)
            {
                var menuItem = new NativeItem($"{item.Item.Name} x{item.Count}");

                menuItem.Activated += (sender, e) =>
                {
                    if (item.Item.Use() == ItemResult.Consume)
                    {
                        item.Consume();
                    }
                };

                inventoryMenu.Add(menuItem);
            }
        }

        internal static void TriggerUse()
        {

        }
    }
}
