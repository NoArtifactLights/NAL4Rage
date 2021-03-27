// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using NALRage.Engine.Modification.API.Events;
using NALRage.Entities;
using Rage;
using System;
using System.Collections.Generic;
using NALRage.Engine.Modification.API;
using Rage.Attributes;

namespace NALRage.Engine.Modification.GameFibers
{
    internal static class GameManager
    {
        private static readonly List<PoolHandle> ProcessedPeds = new List<PoolHandle>();
        private static readonly List<PoolHandle> KilledPeds = new List<PoolHandle>();
        private static bool forceEvent;
        
        internal static readonly List<Vector3> Garages = new List<Vector3>();

        [ConsoleCommand(Description = "Forces a event to be started in next NAL tick.")]
        public static void ForceStartEvent()
        {
            forceEvent = true;
        }
        
        internal static void ProcessEach100()
        {
            while (Common.InstanceRunning)
            {
                GameFiber.Sleep(100);
                if (Game.LocalPlayer.WantedLevel != 0) // Avoid unnecessary set native
                {
                    Game.LocalPlayer.WantedLevel = 0;
                }

                var peds = World.GetAllPeds();
                foreach (var p in peds)
                {
                    GameFiber.Yield();
                    if (!p.Exists()) continue;
                    // Detects whether a ped was killed by the player
                    if (p.HasBeenDamagedBy(Game.LocalPlayer.Character) && p.IsDead && !KilledPeds.Contains(p.Handle))
                    {
                        KilledPeds.Add(p.Handle);
                        Common.Kills++;
                        Common.Cash += new Random().Next(5, 15);
                        if (Entry.ArmedIds.Contains(p.Handle))
                        {
                            Common.Cash += Common.BountyBonus;
                            Game.DisplayHelp("Bounty bonus +$" + Common.BountyBonus);
                        }

                        // Checks and removes it's blip as this ped is currently dead.
                        if (p.GetAttachedBlip().Exists())
                        {
                            p.GetAttachedBlip().Delete();
                        }

                        DetermineDiff();
                    }
                }

                foreach (var eventPed in peds)
                {
                    if (!eventPed.Exists()) continue;
                    // Avoid animals to be flagged
                    if (!eventPed.IsHuman) continue;

                    var random = new Random().Next(9, 109);
                    if ((random == 89 || forceEvent) && !ProcessedPeds.Contains(eventPed.Handle) &&
                        !Entry.ArmedIds.Contains(eventPed.Handle) &&
                        eventPed.Model.Name != "s_m_y_cop_01" && eventPed.Model.Name != "s_f_y_cop_01" &&
                        eventPed.RelationshipGroup != RelationshipGroup.Cop && !eventPed.IsPlayer)
                    {
                        ProcessedPeds.Add(eventPed.Handle);
                        EventManager.StartRandomEvent(eventPed);
                    }

                    forceEvent = false;
                }

                EventManager.Process();
                if (ProcessedPeds.Count == 10000)
                {
                    Logger.Trace("Game", "Cleaning IDs");
                    ProcessedPeds.Clear();
                }

                if (Entry.ArmedIds.Count == 1000)
                {
                    Logger.Trace("Game", "Cleaning armed ids");
                    Entry.ArmedIds.Clear();
                }

                if (KilledPeds.Count == 100)
                {
                    Logger.Trace("Game", "Cleaning killed ids");
                    KilledPeds.Clear();
                }
            }
        }

        private static void DetermineDiff()
        {
            switch (Common.Kills)
            {
                case 1:
                    Logger.Trace("Game", "First Blood!");
                    Game.DisplayHelp("You just killed a person. Once you killed amount of person, the difficulty will raise.");
                    break;

                case 100:
                    Logger.Info("Game", "Difficulty has been altered to Easy");
                    Common.Difficulty = Difficulty.Easy;
                    Game.DisplayHelp("You are current on Easy.");
                    GameContentUtils.SetRelationship(Difficulty.Easy);
                    break;


                case 300:
                    Logger.Info("Game", "Difficulty has been altered to Normal");
                    Common.Difficulty = Difficulty.Normal;
                    Game.DisplayHelp("You are current on Normal.");
                    GameContentUtils.SetRelationship(Difficulty.Normal);
                    break;

                case 700:
                    Logger.Info("Game", "Difficulty has been altered to Hard");
                    Common.Difficulty = Difficulty.Hard;
                    Game.DisplayHelp("You are current on Hard.");
                    GameContentUtils.SetRelationship(Difficulty.Hard);
                    break;

                case 1500:
                    Logger.Info("Game", "Difficulty has been altered to Extreme");
                    Common.Difficulty = Difficulty.Extreme;
                    Game.DisplayHelp("You are current on Extreme.");
                    GameContentUtils.SetRelationship(Difficulty.Extreme);
                    break;
            }
        }
    }
}
