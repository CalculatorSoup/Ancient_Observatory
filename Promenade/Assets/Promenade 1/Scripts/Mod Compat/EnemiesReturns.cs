using EnemiesReturns;
using EnemiesReturns.Configuration;
using EnemiesReturns.Configuration.LynxTribe;
using EnemiesReturns.Enemies.Colossus;
using EnemiesReturns.Enemies.LynxTribe;
using EnemiesReturns.Enemies.LynxTribe.Scout;
using EnemiesReturns.Enemies.LynxTribe.Shaman;
using EnemiesReturns.Enemies.LynxTribe.Totem;
using EnemiesReturns.Enemies.Spitter;
using EnemiesReturns.Enemies.Swift;
using R2API;
using RoR2;
using SS2.Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Promenade
{
    public class EnemiesReturnsCompat
    {
        public static void AddEnemies()
        {
            // Colossus
            if (Promenade.toggleColossus.Value && General.EnableColossus.Value)
            {
                var colossusCard = new RoR2.DirectorCard()
                {
                    spawnCard = (RoR2.SpawnCard)(object)ColossusBody.SpawnCards.cscColossusGrassy,
                    spawnDistance = RoR2.DirectorCore.MonsterSpawnDistance.Standard,
                    selectionWeight = Colossus.SelectionWeight.Value,
                    minimumStageCompletions = Colossus.MinimumStageCompletion.Value
                };

                var colossusHolder = new DirectorAPI.DirectorCardHolder
                {
                    Card = colossusCard,
                    MonsterCategory = DirectorAPI.MonsterCategory.Champions
                };

                if (!Colossus.DefaultStageList.Value.Contains("observatory_wormsworms")) //Checking whether default stage list has Colossi to avoid adding a duplicate spawn card
                {
                    DirectorAPI.Helpers.AddNewMonsterToStage(colossusHolder, false, DirectorAPI.Stage.Custom, "observatory_wormsworms");
                    Log.Info("Colossus added to ancient observatory's spawn pool.");
                }
                if (!Colossus.DefaultStageList.Value.Contains("itobservatory_wormsworms"))
                {
                    DirectorAPI.Helpers.AddNewMonsterToStage(colossusHolder, false, DirectorAPI.Stage.Custom, "itobservatory_wormsworms");
                    Log.Info("Colossus added to ancient observatory's simulacrum spawn pool.");
                }

            }

            // Swift
            if (Promenade.toggleSwift.Value && General.EnableSwift.Value)
            {
                var swiftCard = new RoR2.DirectorCard()
                {
                    spawnCard = (RoR2.SpawnCard)(object)SwiftBody.SpawnCards.cscSwiftDefault,
                    spawnDistance = RoR2.DirectorCore.MonsterSpawnDistance.Far,
                    selectionWeight = Swift.SelectionWeight.Value,
                    minimumStageCompletions = Swift.MinimumStageCompletion.Value
                };

                var swiftHolder = new DirectorAPI.DirectorCardHolder
                {
                    Card = swiftCard,
                    MonsterCategory = DirectorAPI.MonsterCategory.BasicMonsters
                };

                if (!Swift.DefaultStageList.Value.Contains("observatory_wormsworms"))
                {
                    DirectorAPI.Helpers.AddNewMonsterToStage(swiftHolder, false, DirectorAPI.Stage.Custom, "observatory_wormsworms");
                    Log.Info("Swift added to ancient observatory's spawn pool.");
                }
                if (!Swift.DefaultStageList.Value.Contains("itobservatory_wormsworms"))
                {
                    //DirectorAPI.Helpers.AddNewMonsterToStage(swiftHolder, false, DirectorAPI.Stage.Custom, "itobservatory_wormsworms");
                    //Log.Info("Swift added to ancient observatory's simulacrum spawn pool.");
                }

            }

        }
    }
}