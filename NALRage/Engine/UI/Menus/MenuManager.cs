// NALRage
// Copyright (C) RelaperCrystal 2020-2021.

using Rage;
using Rage.Native;
using System.Windows.Forms;
using NALRage.Engine.Modification.API;
using LemonUI;
using LemonUI.Menus;
using System;

namespace NALRage.Engine.UI.Menus
{
    /// <summary>
    /// The general handler thread for all menus in NAL.
    /// </summary>
    public static class MenuManager
    {
        internal static ObjectPool Pool { get; set; } = new ObjectPool();
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        private static NativeMenu mainMenu;
        private static NativeItem itemSave;
        private static NativeItem itemLoad;
        private static NativeItem itemCallCops;
        private static NativeItem itemDifficulty;
        private static NativeItem itemKills;
        private static NativeItem itemAppearance;
        private static NativeCheckboxItem itemLights;
        private static NativeItem itemCash;

        private static NativeMenu buyMenu;
        private static NativeItem itemPistol;
        private static NativeItem itemPumpShotgun;
        private static NativeItem itemMicroSMG;
        private static NativeItem itemBodyArmor;

        private static NativeMenu foodMenu;
        private static NativeItem itemHamburger;


#pragma warning disable S125 // Sections of code should not be commented out
                            //private static NativeMenu modelsMenu;
                            //private static NativeItem itemCop;
                            //private static NativeItem itemClassic;
        private static bool noticed;
#pragma warning restore S125 // Sections of code should not be commented out

#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables

        internal static void FiberInit()
        {
            mainMenu = new NativeMenu("NAL", "Main Menu");
            itemLights = new NativeCheckboxItem("Blackout", "Sets whether to turn off power of whole San Andreas.", true);
            itemSave = new NativeItem("Save Game", "Saves the current game status to a save file.");
            itemLoad = new NativeItem("Load Game", "Loads the game status from a save file.");
            itemCallCops = new NativeItem("Call the Cops", "Call for police services.");
            itemDifficulty = new NativeItem("Difficulty", "Views the current difficulty.");
            itemKills = new NativeItem("Kills", "Views the current kill count.");
            itemAppearance = new NativeItem("Appearance", "Obsolete and not available. Will be updated in the new version.")
            {
                Enabled = false
            };

            itemCash = new NativeItem("Cash", "Views the current cash amount.");
            mainMenu.Add(itemLights);
            mainMenu.Add(itemSave);
            mainMenu.Add(itemLoad);
            mainMenu.Add(itemCallCops);
            mainMenu.Add(itemDifficulty);
            mainMenu.Add(itemKills);
            mainMenu.Add(itemAppearance);
            mainMenu.Add(itemCash);
            itemLights.CheckboxChanged += ItemLights_CheckboxEvent;
            itemSave.Activated += ItemSave_Activated;
            itemLoad.Activated += ItemLoad_Activated;
            itemCallCops.Activated += ItemCallCops_Activated;


#pragma warning disable S125 // Sections of code should not be commented out
            //modelsMenu = new NativeMenu("Appearance", "Appearance Menu", "Modifies your appearance.");
            //itemCop = new NativeItem("Male Cop", "The male police officer.");
            //itemClassic = new NativeItem("Classic", "The classic NAL load model.");
            //itemCop.Activated += ItemCop_Activated;
            //itemClassic.Activated += ItemClassic_Activated;
            //modelsMenu.Add(itemClassic);
            //modelsMenu.Add(itemCop);


#pragma warning restore S125 // Sections of code should not be commented out

            Pool.Add(mainMenu);

#pragma warning disable S125 // Sections of code should not be commented out
            //Pool.Add(modelsMenu);
            buyMenu = new NativeMenu("Guns", "Weapon Shop");
#pragma warning restore S125 // Sections of code should not be commented out
            itemPistol = WeaponShopUtils.GenerateWeaponSellerItem("Pistol Ammo x100", "A personal defense weapon that is easy to carry, but has limited clip.", 100);
            itemPumpShotgun = WeaponShopUtils.GenerateWeaponSellerItem("Pump Shotgun Ammo x50", "A weapon has short range but has strong power when enemy comes close.", 150);
            itemMicroSMG = WeaponShopUtils.GenerateWeaponSellerItem("Micro SMG Ammo x50", "Simple automatic weapon for everyone. Can shoot from cars.", 200);
            itemBodyArmor = WeaponShopUtils.GenerateWeaponSellerItem("Standard Body Armor", "This armor can defend one shotgun round in min-range and can defend several pistol rounds.", 350);
            itemPistol.Activated += ItemPistol_Activated;
            itemPumpShotgun.Activated += ItemPumpShotgun_Activated;
            itemMicroSMG.Activated += (sender, e) => WeaponShopUtils.SellWeapon(200, 50, WeaponHash.MicroSMG);
            itemBodyArmor.Activated += ItemBodyArmor_Activated;
            buyMenu.Add(itemPistol);
            buyMenu.Add(itemPumpShotgun);
            buyMenu.Add(itemBodyArmor);

            Pool.Add(buyMenu);

            foodMenu = new NativeMenu("Shop", "Food Menu");
            itemHamburger = new NativeItem("Hamburger");
            foodMenu.Add(itemHamburger);
            Pool.Add(foodMenu);
            while (true)
            {
                GameFiber.Yield();
                Pool.Process();

                if (!noticed)
                {
                    noticed = true;
                }
                if (!buyMenu.Visible)
                {
                    if (Game.IsKeyDown(Keys.N))
                    {
                        Game.LogTrivial("Key is N. hit!");
                        itemCash.AltTitle = Common.Cash.ToString("N") + "$";
                        itemKills.AltTitle = Common.Kills.ToString();
                        itemDifficulty.AltTitle = Common.Difficulty.ToString();
                        mainMenu.Visible = !mainMenu.Visible;
                    }
                    if (Game.IsKeyDown(Keys.E) && WeaponShopUtils.DistanceToAmmu())
                    {
                        buyMenu.Visible = !buyMenu.Visible;
                    }
                }
                if (WeaponShopUtils.DistanceToAmmu())
                {
                    Game.DisplayHelp("Press ~INPUT_CONTEXT~ to buy weapon.");
                }
            }

        }

        //private static void ItemClassic_Activated(object sender, EventArgs e)

#pragma warning disable S125 // Sections of code should not be commented out
        //{
        //    ChangeModel("a_m_m_bevhills_02", itemClassic);
        //}

        //private static void ItemCop_Activated(object sender, EventArgs e)
        //{
        //    ChangeModel("s_m_y_cop_01", itemCop);
        //}

        //        private static void ChangeModel(string model, NativeItem origin)

        //        {
        //            SaveUtils.SaveManager.Save(Common.Blackout);
        //            Game.LocalPlayer.Model = model;
        //            SaveUtils.SaveManager.Load();
        //            itemClassic.Enabled = true;
        //            itemCop.Enabled = true;
        //            origin.Enabled = false;
        //        }

#pragma warning restore S125 // Sections of code should not be commented out

        private static void ItemBodyArmor_Activated(object sender, EventArgs e) => WeaponShopUtils.SellArmor(70, 350);

        private static void ItemPumpShotgun_Activated(object sender, EventArgs e) => WeaponShopUtils.SellWeapon(200, 50, WeaponHash.PumpShotgun);

        private static void ItemPistol_Activated(object sender, EventArgs e) => WeaponShopUtils.SellWeapon(100, 100, WeaponHash.Pistol);

        private static void ItemCallCops_Activated(object sender, EventArgs e)
        {
            NativeFunction.Natives.CREATE_INCIDENT(7, Game.LocalPlayer.Character.Position.X, Game.LocalPlayer.Character.Position.Y, Game.LocalPlayer.Character.Position.Z, 2, 3.0f, new NativePointer());
        }

        private static void ItemLoad_Activated(object sender, EventArgs e)
        {
            SaveUtils.SaveManager.Load();
        }

        private static void ItemSave_Activated(object sender, EventArgs e)
        {
            SaveUtils.SaveManager.Save(Common.Blackout);
        }

        private static void ItemLights_CheckboxEvent(object sender, EventArgs e) => Functions.BlackoutStatus = itemLights.Checked;
    }
}