// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using NALRage.Engine;
using NALRage.Engine.Extensions;
using NALRage.Engine.UI.Menus;
using NALRage.Engine.Modification;
using NALRage.Engine.Modification.API;
using NALRage.Engine.Modification.API.Events;
using NALRage.Engine.Modification.API.Events.Integrated;
using NALRage.Engine.Modification.GameFibers;
using NALRage.Entities;
using NALRage.Entities.Serialization;
using Rage;
using Rage.Attributes;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Threading;
using NALRage.Engine.Modification.Custom;
using NALRage.Engine.Modification.Character;

// Experimental, May Fail!
[assembly: Plugin("NoArtifactLights", Author = "RelaperCrystal", Description = "The NoArtifactLights Project for RAGE Plug-in Hook", EntryPoint = "NALRage.Entry.Main", PrefersSingleInstance = true)]

namespace NALRage
{
    /// <summary>
    /// The entry point of the mod.
    /// Avoid calls to this class from external plug-in.
    /// </summary>
    public static class Entry
    {
        internal static readonly List<uint> ArmedIds = new List<uint>();
        internal static readonly List<Blip> Blips = new List<Blip>();
        
        private static Configuration config;
        private static GameFiber process;
        private static GameFiber shops;

        [ConsoleCommand(Name = "ReloadConfigs", Description = "Reloads configuration of NAL.")]
        private static void GetConfig()
        {
            ConfigurationHandler.Init();
            config = ConfigurationHandler.Config;
        }

        /// <summary>
        /// The initializer method of the NAL.
        /// <b>Do not</b> call directly. This will break the whole game.
        /// </summary>
#pragma warning disable S4210 // Windows Forms entry points should be marked with STAThread
        public static void Main()
#pragma warning restore S4210 // Windows Forms entry points should be marked with STAThread
        {
            try
            {
                Logger.Info("Main", "Initializing NAL...");
                GetConfig();
                Logger.Info("Main", "Setting prop density and loading GTAO map...");

                NativeFunction.Natives.x808519373FD336A3(true); // SetPlayerIsInDirectorMode, so we hide the name
                NativeFunction.Natives.x0888C3502DBBEEF5(); // ON_ENTER_MP, so we load the map
                NativeFunction.Natives.x9BAE5AD2508DF078(1); // SET_INSTANCE_PRIORITY_MODE, so we set the props
                Functions.IsInRiot = ConfigurationHandler.Config.Riot;

                Logger.Info("Main", "Map loaded. Changing player model...");
#if DEBUG
                var model = new Model("mp_f_freemode_01"); // request the model (for god sake)
                model.LoadAndWait();
                Game.LocalPlayer.Model = model;
                Game.LogTrivial("WARNING: IF YOU ARE STILL IN CONSOLE, EXIT THE CONSOLE, NOW!!!");
                Game.LogTrivial("WARNING: IF YOU ARE STILL IN CONSOLE, EXIT THE CONSOLE, NOW!!!");
                Game.LogTrivial("WARNING: IF YOU ARE STILL IN CONSOLE, EXIT THE CONSOLE, NOW!!!");
                GameFiber.Wait(500);
                Game.LocalPlayer.Character.IsVisible = true; // It has to be set to visible manually
                model.Dismiss();
                NextGenCharacter.ApplyNextGenCharFeatures(Game.LocalPlayer.Character);
#else
                Game.LocalPlayer.Model = "a_m_m_bevhills_02";
#endif
                Game.FadeScreenOut(1000);
                GameFiber.Sleep(1000);
                Logger.Info("Main", "Changed the model, setting up game");
                Game.LocalPlayer.Character.Position = new Vector3(459.8501f, -1001.404f, 24.91487f);
                Game.LocalPlayer.Character.Inventory.GiveFlashlight();
                Game.LocalPlayer.Character.Inventory.GiveNewWeapon(WeaponHash.Pistol, 50, true);
                Game.MaxWantedLevel = 0;
                GameContentUtils.SetRelationship(Difficulty.Initial);
                Functions.BlackoutStatus = true;
                EventManager.RegisterEvent(typeof(ArmedPed));

                Logger.Info("Main", "GameFiber > MenuManager.FiberInit > Creating & Starting Instance");
                GameFiber.StartNew(MenuManager.FiberInit, "MenuManager");
                Logger.Info("Main", "GameFiber > GameManager.ProcessEach100 > Creating & Starting Instance");
                process = GameFiber.StartNew(GameManager.ProcessEach100, "Process");
                Logger.Info("Main", "GameFiber > HungryManager.FiberNew > Creating & Start Instance");
                HungryUtils.StartFiber();
                Logger.Info("Main", "GameFiber > ShopManager.Fiber > Creating & Starting Instance");
                shops = GameFiber.StartNew(ShopManager.Loop, "ShopManager");
                Logger.Info("Main", "GameFiber > RespawnManager.Loop() > Starting");
                GameFiber.StartNew(RespawnManager.Loop);

                GameFiber.Sleep(5000);
                Game.FadeScreenIn(1000);
#if DEBUG
                Logger.Info("Main", "Loading Plug-ins");
                PluginManager.LoadPlugins();
#endif
                Logger.Info("Main", "DONE!");
                Game.DisplayHelp("Welcome to NoArtifactLights!");

                NativeFunction.Natives.x92F0DA1E27DB96DC(210); // set notification colors
                Game.DisplayNotification("You have currently playing the ~h~RAGE Plug-in Hook~s~ version.");

                NativeFunction.Natives.x92F0DA1E27DB96DC(6);
                Game.DisplayNotification("~h~WARNING~w~: PoC next-gen character is active.");

                GameFiber.Hibernate();
            }
            catch (ThreadAbortException)
            {
                Logger.Info("Main", "Someone is aborting our thread");
            }
            catch (Exception ex)
            {
                Game.DisplayHelp("There was an error prevents NAL from loading. See the log for more information.");
                Game.LogTrivial(ex.ToString());
                Game.LogTrivial("------------------- END OF CURRENT STATE -------------------");
                throw;
            }
            finally
            {
                Common.InstanceRunning = false;
                PluginManager.Finally();
            }
        }

        /// <summary>
        /// The unloading method of the mod.
        /// Do <b>not</b> call directly.
        /// </summary>
        /// <param name="crashed"></param>
        public static void OnUnload(bool crashed)
        {
            if (crashed)
            {
                Logger.Fatal("Main", "RAGE Plug-in Hook is shutting down the mod because an un-handled exception.");
                Logger.Fatal("Main", "You are advised to check the log.");
            }

            foreach (var blip in Blips)
            {
                if (blip)
                {
                    blip.Delete();
                }
            }

            Functions.IsInRiot = false;
        }
    }
}
