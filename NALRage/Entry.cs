// Copyright (C) Hot Workshop & contributors 2020.
// Licensed under GNU General Public License version 3.

using NALRage.Engine;
using NALRage.Engine.Menus;
using NALRage.Engine.Modification;
using NALRage.Engine.Modification.API;
using NALRage.Engine.Modification.GameFibers;
using NALRage.Entities;
using NALRage.Entities.Serialization;
using Rage;
using Rage.Attributes;
using Rage.Native;
using System;
using System.Collections.Generic;
                                                                                                                                     // Experimental, May Fail!
[assembly: Plugin("NoArtifactLights", Author = "RelaperCrystal", Description = "The NoArtifactLights Project for RAGE Plug-in Hook", EntryPoint = "NALRage.Entry.Main", PrefersSingleInstance = true)]

namespace NALRage
{
    public static class Entry
    {
        // TODO make this for game manager to use
        internal static List<uint> ArmedIds = new List<uint>();
        
        internal static Configuration Config;
        private static GameFiber process;
        private static bool enabled = true; // TODO deprecate this

        [ConsoleCommand(Name = "ReloadConfigs", Description = "Reloads configuration of NAL.")]
        private static void GetConfig()
        { 
            ConfigurationHandler.Init();
            Config = ConfigurationHandler.Config;
        }

        // Initializer Method
        public static void Main()
        {
            try
            {
                Game.FadeScreenOut(1000);
                GameFiber.Sleep(1000);
                Logger.Info("Main", "Initializing NAL...");
                GetConfig();
                Logger.Info("Main", "Setting prop density and loading GTAO map...");

                NativeFunction.Natives.x0888C3502DBBEEF5(); // ON_ENTER_MP
                NativeFunction.Natives.x9BAE5AD2508DF078(1); // SET_INSTANCE_PRIORITY_MODE

                Logger.Info("Main", "Map loaded. Changing player model...");
                Game.LocalPlayer.Model = "a_m_m_bevhills_02";
                Logger.Info("Main", "Changed the model, setting up game");
                Game.LocalPlayer.Character.Position = new Vector3(459.8501f, -1001.404f, 24.91487f);
                Game.LocalPlayer.Character.Inventory.GiveFlashlight();
                Game.LocalPlayer.Character.Inventory.GiveNewWeapon(WeaponHash.Pistol, 50, true);
                Game.MaxWantedLevel = 0;
                GameContentUtils.SetRelationship(Difficulty.Initial);
                NativeFunction.Natives.x1268615ACE24D504(true);
                Logger.Info("Main", "GameFiber > MenuManager.FiberInit > Creating Instance");
                GameFiber menu = new GameFiber(MenuManager.FiberInit);
                Logger.Info("Main", "GameFiber > MenuManager.FiberInit > Calling Start");
                menu.Start();
                Logger.Info("Main", "GameFiber > GameManager.ProcessEach100 > Creating & Starting Instance");
                process = GameFiber.ExecuteNewFor(GameManager.ProcessEach100, 100);
                GameFiber.Sleep(5000);
                Game.FadeScreenIn(1000);
                Logger.Info("Main", "DONE!");
                Game.DisplayHelp("Welcome to NoArtifactLights!");
                Game.DisplayNotification("You have currently playing the ~h~RAGE Plug-in Hook~s~ version.");
                GameFiber.Hibernate();
            }
            catch(Exception ex)
            {
                CrashReporter cr = new CrashReporter(ex);
                cr.ReportAndCrashPlugin();
            }
        }
    }
}
