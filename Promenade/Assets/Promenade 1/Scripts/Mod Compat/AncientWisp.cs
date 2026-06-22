using R2API;
using RoR2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientWisp;

namespace Promenade
{
    public class AncientWispCompat
    {
        public static void AddEnemies()
        {
            var spawnInfo = new AncientWisp.StageSpawnInfo("observatory_wormsworms", 0);
            var spawnInfoLoop = new AncientWisp.StageSpawnInfo("observatory_wormsworms", 5);

            var simuSpawnInfo = new AncientWisp.StageSpawnInfo("itobservatory_wormsworms", 0);
            var simuSpawnInfoLoop = new AncientWisp.StageSpawnInfo("itobservatory_wormsworms", 5);

            if (Promenade.toggleAncientWisp.Value && !AncientWispPlugin.StageList.Contains(spawnInfo) && !AncientWispPlugin.StageList.Contains(spawnInfoLoop)) //checking if the stage isn't already in the stage list to avoid adding an extra spawn card
            {
                DirectorAPI.Helpers.AddNewMonsterToStage(AncientWisp.AWContent.AncientWispCard, false, DirectorAPI.Stage.Custom, "observatory_wormsworms");
                //Log.Info("Ancient Wisp added to Hollow Crest's spawn pool.");
            }
            if (Promenade.toggleAncientWisp.Value && !AncientWispPlugin.StageList.Contains(simuSpawnInfo) && !AncientWispPlugin.StageList.Contains(simuSpawnInfoLoop))
            {
                DirectorAPI.Helpers.AddNewMonsterToStage(AncientWisp.AWContent.AncientWispCard, false, DirectorAPI.Stage.Custom, "itobservatory_wormsworms");
                //Log.Info("Ancient Wisp added to Hollow Crest's Simulacrum spawn pool.");
            }
        }
    }
}