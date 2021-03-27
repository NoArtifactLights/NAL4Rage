// Copyright (C) Hot Workshop & contributors 2020, 2021.
// Licensed under GNU General Public License version 3.

using NALRage.Entities;
using Rage;
using System;
using System.Drawing;

namespace NALRage.Engine
{
    /// <summary>
    /// Provides methods to handle game contents.
    /// </summary>
    public static class GameContentUtils
    {
        private static readonly RelationshipGroup CivilianMale = new RelationshipGroup("CIVMALE");

        /// <summary>
        /// Updates the relationship status according to the specified difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public static void SetRelationship(Difficulty difficulty)
        {
            Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Player, RelationshipGroup.Cop, Relationship.Companion);
            Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RelationshipGroup.Player, Relationship.Companion);
            switch (difficulty)
            {
                case Difficulty.Easy:
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangFamily, RelationshipGroup.AmbientGangBallas, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangBallas, RelationshipGroup.AmbientGangFamily, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Player, Relationship.Hate);
                    break;

                case Difficulty.Normal:
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangFamily, RelationshipGroup.AmbientGangBallas, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangBallas, RelationshipGroup.AmbientGangFamily, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Player, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Cop, Relationship.Hate);
                    break;

                case Difficulty.Hard:
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangFamily, RelationshipGroup.AmbientGangBallas, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangBallas, RelationshipGroup.AmbientGangFamily, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Player, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Cop, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.AmbientGangBallas, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.AmbientGangFamily, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangBallas, CivilianMale, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangFamily, CivilianMale, Relationship.Hate);
                    break;

                case Difficulty.Extreme:
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangFamily, RelationshipGroup.AmbientGangBallas, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangBallas, RelationshipGroup.AmbientGangFamily, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Player, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.Cop, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.AmbientGangBallas, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.AmbientGangFamily, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangBallas, CivilianMale, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.AmbientGangFamily, CivilianMale, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(CivilianMale, RelationshipGroup.DomesticAnimal, Relationship.Hate);
                    break;
            }
        }

        internal static void EquipWeapon(this Ped ped)
        {
            if (!ped.Exists())
            {
                return;
            }
            ped.IsPersistent = true;
            WeaponHash wp;
            switch (Common.Difficulty)
            {
                default:
                    wp = new Random().Next(200, 272) == 40 ? WeaponHash.PumpShotgun : WeaponHash.Pistol;
                    break;

                case Difficulty.Easy:
                    wp = WeaponHash.PumpShotgun;
                    break;

                case Difficulty.Normal:
                    wp = WeaponHash.MicroSMG;
                    break;

                case Difficulty.Hard:
                    wp = WeaponHash.CarbineRifle;
                    break;

                case Difficulty.Extreme:
                    wp = WeaponHash.RPG;
                    break;
            }
            ped.Inventory.GiveNewWeapon(wp, short.MaxValue, true);
            if (ped.IsInAnyVehicle(false))
            {
                ped.Tasks.LeaveVehicle(ped.CurrentVehicle, LeaveVehicleFlags.LeaveDoorOpen);
                GameFiber.Wait(1);
            }
            ped.Dismiss();
            ped.KeepTasks = true;
            Blip b = ped.AttachBlip();
            b.IsFriendly = false;
            b.Sprite = BlipSprite.Enemy;
            b.Scale = 0.5f;
            b.Color = Color.Red;
        }
    }
}