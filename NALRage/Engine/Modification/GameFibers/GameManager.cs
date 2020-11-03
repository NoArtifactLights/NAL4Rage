﻿using NALRage.Engine.Modification.API.Events;
using NALRage.Entities;
using Rage;
using System;
using System.Collections.Generic;
using NALRage.Engine.Modification.API;

namespace NALRage.Engine.Modification.GameFibers
{
    internal static class GameManager
    {
        private static List<PoolHandle> peds = new List<PoolHandle>();
        // private static List<PoolHandle> armedPeds = new List<PoolHandle>();
        private static List<PoolHandle> killedPeds = new List<PoolHandle>();

        internal static void ProcessEach100()
        {
            
            while(true)
            {
                if(Game.IsKeyDown(System.Windows.Forms.Keys.F5))
                {
                    Entry.debugScreen = !Entry.debugScreen;
                }
                GameFiber.Sleep(99);
                GameFiber.Yield();
                Ped[] peds = World.GetAllPeds();
                foreach (Ped p in peds)
                {
                    GameFiber.Yield();
                    if (!p.Exists()) continue;
                    // Detects whether a ped was killed by the player
                    if (p.HasBeenDamagedBy(Game.LocalPlayer.Character) && p.IsDead && !killedPeds.Contains(p.Handle))
                    {
                        killedPeds.Add(p.Handle);
                        Common.Kills++;
                        Common.Cash += new Random().Next(5, 15);
                        if (Entry.ArmedIds.Contains(p.Handle))
                        {
                            Common.Cash += Common.ArmedBonus;
                            Game.DisplayHelp("Kill armed ped bonus +$" + Common.ArmedBonus);
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

                    int var = new Random().Next(9, 889);
                    if (var == 89 && !Entry.ArmedIds.Contains(p2.Handle) && !(p2.Model.Name == "s_m_y_cop_01" || p2.Model.Name == "s_f_y_cop_01") && !p2.IsPlayer)
                    {
                        EventManager.StartRandomEvent(p2);
                    }

                }
                if (GameManager.peds.Count == 10000)
                {
                    Logger.Trace("Game", "Cleaning IDs");
                    GameManager.peds.Clear();
                }
                if (Entry.ArmedIds.Count == 1000)
                {
                    Logger.Trace("Game", "Cleaning armed ids");
                    Entry.ArmedIds.Clear();
                }
                if (killedPeds.Count == 100)
                {
                    Logger.Trace("Game", "Cleaning killed ids");
                    killedPeds.Clear();
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
                    Logger.Info("Game", "Difficulty has been altered to Nether");
                    Common.Difficulty = Difficulty.Nether;
                    Common.BigMessage.MessageInstance.ShowSimpleShard("Difficulty Changed", "Difficulty is changed to Nether.");
                    GameContentUtils.SetRelationship(Difficulty.Nether);
                    break;
            }
        }
    }
}
