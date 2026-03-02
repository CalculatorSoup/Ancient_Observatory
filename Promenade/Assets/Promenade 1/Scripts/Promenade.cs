using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using FSCStage;
using HG;
using Promenade.Content;
using R2API;
using R2API.AddressReferencedAssets;
using R2API.Utils;
using RoR2;
using RoR2.ContentManagement;
using RoR2BepInExPack.GameAssetPaths;
using RoR2BepInExPack.GameAssetPathsBetter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Diagnostics;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//Copied from Wetland Downpour copied from Fogbound Lagoon copied from Nuketown


#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[assembly: HG.Reflection.SearchableAttribute.OptIn]

namespace Promenade
{
    [BepInPlugin(GUID, Name, Version)]
    public class Promenade : BaseUnityPlugin
    {
        public const string Author = "wormsworms";

        public const string Name = "Ancient_Observatory";

        public const string Version = "1.0.0";

        public const string GUID = Author + "." + Name;

        public static Promenade instance;

        public static ConfigEntry<bool> enableRegular;
        public static ConfigEntry<bool> enableSimulacrum;
        public static ConfigEntry<bool> stage1Simulacrum;

        public static ConfigEntry<bool> toggleSwift;
        public static ConfigEntry<bool> toggleColossus;

        public static ConfigEntry<bool> toggleCannonballJellyfish;

        public static ConfigEntry<bool> toggleBrassMonolith;

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);

            ConfigSetup();

            ContentManager.collectContentPackProviders += GiveToRoR2OurContentPackProviders;

            RoR2.Language.collectLanguageRootFolders += CollectLanguageRootFolders;

            SceneManager.sceneLoaded += ArtifactTeleporterSetup;

            RoR2.RoR2Application.onLoadFinished += AddModdedEnemies;

        }

        public static void AddModdedEnemies()
        {
            if (IsEnemiesReturns.enabled)
            {
                EnemiesReturnsCompat.AddEnemies(); //Colossus, Swift
            }

            if (IsSandswept.enabled)
            {
                SandsweptCompat.AddEnemies(); //Cannonball Jellyfish
            }

            if (IsForgottenRelics.enabled)
            {
                ForgottenRelicsCompat.AddEnemies(); //Brass Monolith
            }
        }

        // Instantiate Artifact Portal doesn't include props, and some artifact portal props aren't available as prefabs, so instead I just add the prefab with the teleporter + sky meadow island
        // and hide the objects I don't want
        public void ArtifactTeleporterSetup(Scene newScene, LoadSceneMode loadSceneMode)
        {
            if (newScene.name == "observatory_wormsworms")
            {
                string[] islandObjectNames =
                    { "MS_FloatingIsland1", "Final Zone/Grass", "ChainlinkSet", "ChainlinkSet (1)", "ChainlinkSet (2)", "ChainlinkSet (3)", "TP Area Holder/MiscProps",
                "LShapeScaffolding", "StaircaseScaffolding", "Formula/spmSMGrassSmallCluster", "Formula/spmSMGrassSmallCluster (1)", "Formula/spmSMGrassSmallCluster (2)",
                "Formula/spmSMFruitPlant", "PortalDialer/spmSMGrassSmallCluster (3)", "PortalDialer/spmSMGrassSmallCluster (4)",
                "PortalDialerButton 1", "PortalDialerButton 2", "PortalDialerButton 3", "PortalDialerButton 4", "PortalDialerButton 5", "PortalDialerButton 6", "PortalDialerButton 7",
                "PortalDialerButton 8", "PortalDialerButton 9", "PortalDialer", "Final Zone/MiscProps"
                };

                //Deactivate and/or destroy objects
                foreach (string objectName in islandObjectNames)
                {
                    if (GameObject.Find(objectName) != null)
                    {
                        //GameObject.Find(objectName).SetActive(false);
                        UnityEngine.Object.Destroy(GameObject.Find(objectName));
                    }
                }

                //Changing material of various objects to better fit the stage
                Material metalMat = PromenadeContent.MetalMaterial;

                GameObject powerlineHolder1 = GameObject.Find("Final Zone/PowerLines");
                string[] miscMetalItems = { "LOP_ArtifactLaptop(Clone)/FW_Crate", "mega teleporter/PowerLine, Huge", "Powerline/SM_PowerLine(Clone)", "FW_CellTowerSnowy(Clone)/HumanLargeCellTowerMesh", "LOP_ArtifactLaptop(Clone)/FW_Crate", "PowerCoil, 1/PowerLine1 (1)" };

                for (int i = 0; i < powerlineHolder1.transform.childCount; i++)
                {
                    if (powerlineHolder1.transform.GetChild(i).name.Contains("PowerLine"))
                    {
                        powerlineHolder1.transform.GetChild(i).TryGetComponent(out MeshRenderer childMeshRenderer);
                        childMeshRenderer.material = metalMat;
                    }
                }
                foreach (string objectName in miscMetalItems)
                {
                    if (GameObject.Find(objectName) != null)
                    {
                        GameObject.Find(objectName).TryGetComponent(out MeshRenderer meshRenderer);
                        meshRenderer.material = metalMat;
                    }
                }

            }
        }



        private void Destroy()
        {
            RoR2.Language.collectLanguageRootFolders -= CollectLanguageRootFolders;
        }

        private static void GiveToRoR2OurContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(new ContentProvider());
        }

        public void CollectLanguageRootFolders(List<string> folders)
        {
            folders.Add(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(base.Info.Location), "Language"));
            folders.Add(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(base.Info.Location), "Plugins/Language"));
        }

        private void ConfigSetup()
        {
            enableRegular =
                base.Config.Bind<bool>("00 - Stages",
                                       "Enable Ancient Observatory",
                                       true,
                                       "If set to false, Ancient Observatory will never appear in regular runs.");
            enableSimulacrum =
                base.Config.Bind<bool>("00 - Stages",
                                       "Enable Simulacrum Variant",
                                       true,
                                       "If set to false, Ancient Observatory will never appear in the Simulacrum.");
            stage1Simulacrum =
                base.Config.Bind<bool>("00 - Stages",
                                       "Enable Simulacrum Variant on Stage 1",
                                       true,
                                       "If set to false, Ancient Observatory will only appear after clearing at least one stage in the Simulacrum.");
            toggleSwift =
                base.Config.Bind<bool>("01 - Monsters: EnemiesReturns",
                                       "Enable Swift",
                                       true,
                                       "If set to false, Swifts will not appear in Ancient Observatory.");
            toggleColossus =
                base.Config.Bind<bool>("01 - Monsters: EnemiesReturns",
                                       "Enable Colossus",
                                       true,
                                       "If set to false, Colossi will not appear in Ancient Observatory.");
            toggleCannonballJellyfish =
                base.Config.Bind<bool>("03 - Monsters: Sandswept",
                                       "Enable Cannonball Jellyfish",
                                       true,
                                       "If set to false, Cannonball Jellyfish will not appear in Ancient Observatory.");
            toggleBrassMonolith =
                base.Config.Bind<bool>("02 - Monsters: Forgotten Relics",
                                       "Enable Brass Monolith",
                                       true,
                                       "If set to false, Brass Monoliths will not appear in Ancient Observatory.");
        }




    }
}
