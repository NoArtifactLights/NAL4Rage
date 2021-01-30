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
        // private static List<PoolHandle> armedPeds = new List<PoolHandle>();
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
                //if (Game.IsKeyDown(System.Windows.Forms.Keys.F5))
                //{
                //    Entry.debugScreen = !Entry.debugScreen;
                //}
                GameFiber.Sleep(100);
                Ped[] peds = World.GetAllPeds();
                foreach (Ped p in peds)
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

                foreach (Ped p2 in peds)
                {
                    if (!p2.Exists()) continue;
                    // Avoid animals to be flagged
                    if (!p2.IsHuman) continue;

                    var random = new Random().Next(9, 109);
                    if ((random == 89 || forceEvent) && !ProcessedPeds.Contains(p2.Handle) &&
                        !Entry.ArmedIds.Contains(p2.Handle) &&
                        !(p2.Model.Name == "s_m_y_cop_01" || p2.Model.Name == "s_f_y_cop_01") && !p2.IsPlayer)
                    {
                        ProcessedPeds.Add(p2.Handle);
                        EventManager.StartRandomEvent(p2);
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
                    Common.BigMessage.MessageInstance.ShowSimpleShard("Difficulty Changed", "Difficulty is changed to Easy.");
                    GameContentUtils.SetRelationship(Difficulty.Easy);
                    break;

                case 300:
                    Logger.Info("Game", "Difficulty has been altered to Normal");
                    Common.Difficulty = Difficulty.Normal;
                    Common.BigMessage.MessageInstance.ShowSimpleShard("Difficulty Changed", "Difficulty is changed to Normal.");
                    GameContentUtils.SetRelationship(Difficulty.Normal);
                    break;

                case 700:
                    Logger.Info("Game", "Difficulty has been altered to Hard");
                    Common.Difficulty = Difficulty.Hard;
                    Common.BigMessage.MessageInstance.ShowSimpleShard("Difficulty Changed", "Difficulty is changed to Hard.");
                    GameContentUtils.SetRelationship(Difficulty.Hard);
                    break;

                case 1500:
                    Logger.Info("Game", "Difficulty has been altered to Extreme");
                    Common.Difficulty = Difficulty.Extreme;
                    Common.BigMessage.MessageInstance.ShowSimpleShard("Difficulty Changed", "Difficulty is changed to Extreme.");
                    GameContentUtils.SetRelationship(Difficulty.Extreme);
                    break;
            }
        }
    }
}
