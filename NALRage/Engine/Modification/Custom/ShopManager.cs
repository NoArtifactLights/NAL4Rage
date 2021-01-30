using System.IO;
using System.Xml.Serialization;
using NALRage.Engine.Menus;
using NALRage.Engine.Modification.API;
using NALRage.Entities.Serialization.Customization;
using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;

namespace NALRage.Engine.Modification.Custom
{
    // TODO test
    internal static class ShopManager
    {
        private static FoodShop[] shops;
        private static FoodItem[] currentItems;
        private static bool alreadySetCurrentItems;

        private static UIMenu shopMenu;

        internal static void LoadShopManager()
        {
            if (!File.Exists("NAL\\Shops.xml"))
            {
                new CrashReporter(new FileNotFoundException("Invalid foods - copy config file from install archive")).ReportAndCrashPlugin();
            }
            
            var serializer = new XmlSerializer(typeof(ShopsFile));
            var stream = File.OpenRead("NAL\\Shops.xml");
            var instance = (ShopsFile)serializer.Deserialize(stream);
            shops = instance.Shops;
        }

        internal static void LoadShops()
        {
            foreach (var shop in shops)
            {
                var position = new Vector3(shop.Position.X, shop.Position.Y, shop.Position.Z);
                var blip = new Blip(position)
                {
                    Sprite = (BlipSprite)605,
                    Name = "Store"
                };
                shop.Position.GenerateVector3();
                Functions.MarkBlipDeletion(blip);
            }
        }

        internal static void Loop()
        {
            LoadShopManager();
            LoadShops();
            shopMenu = new UIMenu("Food Store", "FOOD STORES");
            MenuManager.Pool.Add(shopMenu);
            
            while (Common.InstanceRunning)
            {
                GameFiber.Yield();
                foreach (var shop in shops)
                {
                    if (Game.LocalPlayer.Character.DistanceTo(shop.Position.GeneratedVector3) < 3.5f)
                    {
                        if (!alreadySetCurrentItems)
                        {
                            currentItems = shop.Items;
                            shopMenu.MenuItems.Clear();
                            foreach (var item in currentItems)
                            {
                                var shopItem = new UIMenuItem(item.Name, "Buys the food.");
                                shopItem.Activated += (sender, selectedItem) =>
                                {
                                    if (Functions.CostMoney(item.Price))
                                    {
                                        Functions.IncreaseHungry(item.Amount);
                                    }
                                };
                                shopMenu.AddItem(shopItem);
                            }
                            alreadySetCurrentItems = true;
                        }
                        
                        Game.DisplayHelp("Press ~INPUT_CONTEXT~ to open the shop menu.");
                        if (Game.IsControlPressed(0, GameControl.Context))
                        {
                            shopMenu.Visible = !shopMenu.Visible;
                        }
                        break;
                    }
                    else
                    {
                        alreadySetCurrentItems = false;
                    }

                    
                }
            }
        }
    }
}