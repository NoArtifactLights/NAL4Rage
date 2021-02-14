using NALRage.Engine.Modification;
using NALRage.Entities.Serialization;
using NALRage.Entities.Serialization.Customization;
using Rage;
using RAGENativeUI.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NALRage.Engine
{
    /// <summary>
    /// Provides methods to handle weapons.
    /// </summary>
    public static class WeaponShopUtils
    {
        private static Vector3[] ammus = { new Vector3(18.18945f, -1120.384f, 28.91654f), new Vector3(-325.6184f, 6072.246f, 31.21228f) };

        /// <summary>
        /// Gets a weapon in the <see cref="WeaponDescriptorCollection"/> by the specified weapon hash.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>A <see cref="WeaponDescriptor"/> with the specified hash.</returns>
        public static WeaponDescriptor GetWeaponFromHash(this WeaponDescriptorCollection collection, WeaponHash hash)
        {
            WeaponDescriptor[] wds;
            IEnumerable<WeaponDescriptor> results =
                from descriptor in collection where descriptor.Hash == hash select descriptor;
            var weaponDescriptors = results as WeaponDescriptor[] ?? results.ToArray();
            if (weaponDescriptors.Count() > 1) throw new InvalidOperationException("Seems like two weapon descriptors with one hash exists!");
            wds = weaponDescriptors.ToArray();
            return wds[0];
        }

        

        internal static UIMenuItem GenerateWeaponSellerItem(string displayName, string description, int price)
        {
            Game.LogTrivial("Creating weapon sell item for: " + displayName + " at price " + price);
            UIMenuItem result = new UIMenuItem(displayName, description);
            result.SetRightLabel("$" + price);
            Game.LogTrivial("Created weapon sell item for: " + displayName);
            return result;
        }

        internal static void LoadShopsFromFile()
        {
            if (!File.Exists("NAL\\WeaponShops.xml"))
            {
                new CrashReporter(new FileNotFoundException("Invalid weapon shops - copy config file from install archive")).ReportAndCrashPlugin();
            }

            var serializer = new XmlSerializer(typeof(PositionsFile));
            var stream = File.OpenRead("NAL\\Shops.xml");
            var instance = (PositionsFile)serializer.Deserialize(stream);

            var shopList = new List<Vector3>();
            foreach (var item in instance.Positions)
            {
                shopList.Add(item);
            }

            ammus = shopList.ToArray();
        }

        internal static bool DistanceToAmmu()
        {
            foreach (Vector3 ammu in ammus)
            {
                if (Game.LocalPlayer.Character.Position.DistanceTo(ammu) < 7f) return true;
            }
            return false;
        }

        internal static void SellWeapon(int price, short ammo, WeaponHash weapon)
        {
            if (Common.Cash < price)
            {
                Game.DisplaySubtitle("You don't have enough money for this.");
                return;
            }
            Common.Cash -= price;
            try
            {
                if (Game.LocalPlayer.Character.Inventory.Weapons.Contains(weapon))
                {
                    WeaponDescriptor wp = Game.LocalPlayer.Character.Inventory.Weapons.GetWeaponFromHash(weapon);
                    if (wp == null)
                    {
                        Game.LocalPlayer.Character.Inventory.GiveNewWeapon(weapon, ammo, true);
                        return;
                    }
                    wp.Ammo += ammo;
                }
                else
                {
                    Game.LocalPlayer.Character.Inventory.GiveNewWeapon(weapon, ammo, true);
                }
            }
            catch
            {
                Common.Cash += price;
            }
        }

        internal static void SellArmor(int amount, int price)
        {
            if (Common.Cash < price)
            {
                Game.DisplaySubtitle("You don't have enough money to buy this.");
                return;
            }
            Common.Cash -= price;
            if (Game.LocalPlayer.Character.Armor >= amount)
            {
                Game.DisplaySubtitle("You already had armor.");
                return;
            }
            Game.LocalPlayer.Character.Armor = amount;
        }
    }
}