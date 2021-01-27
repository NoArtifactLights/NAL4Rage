// Copyright (C) Hot Workshop & contributors 2020.
// Licensed under GNU General Public License version 3.

using NALRage.Engine;
using NALRage.Engine.Extensions;
using NALRage.Engine.Menus;
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
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
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
        // TODO make this for game manager to use
        internal static readonly List<uint> ArmedIds = new List<uint>();
        internal static readonly List<Blip> Blips = new List<Blip>();
        
        private static Configuration config;
        private static GameFiber process;

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

#if DEBUG
                Game.DisplayHelp("You are in debug mode. Please attach a debugger.");
                //Debug.AttachAndBreak();
#endif

                Logger.Info("Main", "Map loaded. Changing player model...");
                Game.LocalPlayer.Model = "a_m_m_bevhills_02";
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
                GameFiber.Sleep(5000);
                Game.FadeScreenIn(1000);
#if DEBUG
                Logger.Info("Main", "Loading Plug-ins");
                PluginManager.LoadPlugins();
#endif
                Logger.Info("Main", "DONE!");
                Game.DisplayHelp("Welcome to NoArtifactLights!");
                Game.DisplayNotification("You have currently playing the ~h~RAGE Plug-in Hook~s~ version.");

                Game.RawFrameRender += Game_RawFrameRender;
                GameFiber.Hibernate();
            }
            catch (ThreadAbortException)
            {
                Logger.Info("Main", "Someone is aborting our thread");
            }
            catch (Exception ex)
            {
                CrashReporter cr = new CrashReporter(ex);
                cr.ReportAndCrashPlugin();
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
            foreach (var blip in Blips)
            {
                if (blip)
                {
                    blip.Delete();
                }
            }
        }

        private static void Game_RawFrameRender(object sender, GraphicsEventArgs e)
        {
            //if (debugScreen)
            //{
            //    e.Graphics.DrawText("NoArtifactLights for Rage", "Courier New", 20f, new PointF(20, 20), Color.Red);
            //    e.Graphics.DrawText("Current event status: ", "Courier New", 20f, new PointF(20, 50), Color.Red);
            //}
            //e.Graphics.DrawText("Temporary Hungry: " + HungryManager.Precentage, "Courier New", 12f, new PointF(20, 20), Color.Red);
        }
    }
}
