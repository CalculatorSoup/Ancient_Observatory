using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using EnemiesReturns.Enemies.Judgement.Arraign;
using EntityStates.NemCaptain.Weapon;
using FRCSharp;
using HG.Reflection;
using Microsoft.CodeAnalysis;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Navigation;
using RoR2BepInExPack.GameAssetPaths.Version_1_35_0;
using RoR2BepInExPack.GameAssetPathsBetter;
using Sandswept;
using SS2;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static R2API.DirectorAPI;

namespace Promenade
{
    public class SandsweptCompat
    {
        public static CharacterSpawnCard CannonballJellyfishCard;

        //go through config files to find the intended monster's "enabled" config, then return its value as a string. return "false" if the config entry can't be found
        public static bool FindEnabledConfig(string configName)
        {
            var configArray = Sandswept.Main.config.GetConfigEntries();
            foreach (var entry in configArray)
            {
                var entryString = entry.Definition.ToString();
                if (entryString == configName)
                {
                    //Log.Debug("Found Config Entry (Enable)");
                    return (bool) entry.BoxedValue;
                }
            }
            return false;
        }
        public static int FindCreditConfig(string configName)
        {
            var configArray = Sandswept.Main.config.GetConfigEntries();
            foreach (var entry in configArray)
            {
                var entryString = entry.Definition.ToString();
                if (entryString == configName)
                {
                    //Log.Debug("Found Config Entry (Credit)");
                    return (int) entry.DefaultValue;
                }
            }
            return 115;
        }

        public static void AddEnemies()
        {
            // Cannonball Jellyfish. Largely copied from MoreVieldsOptions
            var cannonballJellyfishValue = FindEnabledConfig("Enemies :: Cannonball Jellyfish.Enabled");

            if (Promenade.toggleCannonballJellyfish.Value && cannonballJellyfishValue) {
                CannonballJellyfishCard = ScriptableObject.CreateInstance<CharacterSpawnCard>();
                CannonballJellyfishCard.name = "cscAOCannonballJellyfish";
            
                ((SpawnCard) CannonballJellyfishCard).prefab = Sandswept.Main.assets.LoadAsset<GameObject>("CannonJellyMaster.prefab");
                ((SpawnCard) CannonballJellyfishCard).sendOverNetwork = true;
                ((SpawnCard) CannonballJellyfishCard).hullSize = (HullClassification)0;
                ((SpawnCard) CannonballJellyfishCard).nodeGraphType =  RoR2.Navigation.MapNodeGroup.GraphType.Air;
                ((SpawnCard) CannonballJellyfishCard).requiredFlags = (NodeFlags)0;
                ((SpawnCard) CannonballJellyfishCard).forbiddenFlags = (NodeFlags)2;
                ((SpawnCard) CannonballJellyfishCard).directorCreditCost = FindCreditConfig("Enemies :: Cannonball Jellyfish.Director Credit Cost");
                ((SpawnCard) CannonballJellyfishCard).occupyPosition = false;
                CannonballJellyfishCard.loadout = new SerializableLoadout();
                CannonballJellyfishCard.noElites = false;
                CannonballJellyfishCard.forbiddenAsBoss = false;

                DirectorCardHolder cardHolder = new DirectorCardHolder
                    {
                    Card = new DirectorCard
                    {
                        spawnCard = (SpawnCard)(object)CannonballJellyfishCard,
                        selectionWeight = 1
                    },
                    MonsterCategory = (MonsterCategory)2
                };
                DirectorAPI.Helpers.AddNewMonsterToStage(cardHolder, false, DirectorAPI.Stage.Custom, "observatory_wormsworms");
                DirectorAPI.Helpers.AddNewMonsterToStage(cardHolder, false, DirectorAPI.Stage.Custom, "itobservatory_wormsworms");
                Log.Debug("Cannonball Jellyfish added to ancient observatory spawn pool.");
            };
        }
    } 
}