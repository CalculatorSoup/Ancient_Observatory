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

        public const string Version = "0.1.0";

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

            //SceneManager.sceneLoaded += ArtifactTeleporterSetup;

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

        // Currently-unused and extremely messy and unfinished function that was meant to take the artifact TP prefab I instantiate into the map and horribly disfigure it such that
        // clients in multiplayer runs could use the controls. It doesn't work.
        public void ArtifactTeleporterSetup(Scene newScene, LoadSceneMode loadSceneMode)
        {
            if (newScene.name == "observatory_wormsworms")
            {
                string[] islandObjectNames =
                    { "MS_FloatingIsland1", "Final Zone/Grass", "ChainlinkSet", "ChainlinkSet (1)", "ChainlinkSet (2)", "ChainlinkSet (3)", "TP Area Holder/MiscProps",
                "LShapeScaffolding", "StaircaseScaffolding", "Formula/spmSMGrassSmallCluster", "Formula/spmSMGrassSmallCluster (1)", "Formula/spmSMGrassSmallCluster (2)",
                "Formula/spmSMFruitPlant", "PortalDialer/spmSMGrassSmallCluster (3)", "PortalDialer/spmSMGrassSmallCluster (4)",
                "PortalDialerButton 1", "PortalDialerButton 2", "PortalDialerButton 3", "PortalDialerButton 4", "PortalDialerButton 5", "PortalDialerButton 6", "PortalDialerButton 7",
                "PortalDialerButton 8", "PortalDialerButton 9"
                };

                // probably could just do a for loop instead of this
                string[] newDialerNames =
                {
                    "Dialer Button 1",
                    "Dialer Button 2",
                    "Dialer Button 3",
                    "Dialer Button 4",
                    "Dialer Button 5",
                    "Dialer Button 6",
                    "Dialer Button 7",
                    "Dialer Button 8",
                    "Dialer Button 9",
                };

                //Copy values from PortalDialer to my replacement dialer
                //var dialerControllerList = new List<PortalDialerButtonController>();
                var dialerControllerList = new PortalDialerButtonController[9];

                var tpHolder = GameObject.Find("HOLDER: Artifact TP");
                var oldDialer = GameObject.Find("PortalDialer");
                //var newDialer = GameObject.Find("Dialer Replacement");
                //var oldDialer = tpHolder.transform.GetChild(2).GetChild(0).GetChild(2).GetChild(10).gameObject;
                var newDialer = tpHolder.transform.GetChild(2).gameObject;

                Log.Debug(oldDialer.name);
                Log.Debug(newDialer.name);
                NetworkServer.Spawn(newDialer);

                foreach (string dialerName in newDialerNames)
                {
                    var dialerParent = GameObject.Find(dialerName);
                    var dialer = GameObject.Find(dialerName + "/PortalDialerButton(Clone)");
                    if (dialer != null)
                    {
                        //var controller = dialer.GetComponent<PortalDialerButtonController>();
                        dialer.TryGetComponent<PortalDialerButtonController>(out var controller);

                        if (controller != null)
                        {
                            dialerControllerList.Append(controller);
                            Log.Debug("Appended controller to list: " + dialerName);
                            Log.Debug(controller.ToString());
                        } else
                        {
                            Log.Debug("dialer.GetComponent returned null: " + dialerName);
                        }
                    } else
                    {
                        Log.Debug("GameObject.Find returned null: " + dialerName);
                    }
                }
                for (var i = 0; i > dialerControllerList.Length; i++)
                {
                    //newDialer.GetComponent<PortalDialerController>().buttons.SetValue(dialerControllerList.ElementAt(i), i);
                    //newDialer.GetComponent<PortalDialerController>().dialingOrder.SetValue(dialerControllerList.ElementAt(i), i);

                    //oldDialer.GetComponent<PortalDialerController>().buttons.ElementAt(i). = dialerControllerList.ElementAt(i);
                    //oldDialer.GetComponent<PortalDialerController>().dialingOrder.ElementAt(i) = dialerControllerList.ElementAt(i);
                    oldDialer.GetComponent<PortalDialerController>().buttons.SetValue(dialerControllerList.ElementAt(i), i);
                    oldDialer.GetComponent<PortalDialerController>().dialingOrder.SetValue(dialerControllerList.ElementAt(i), i);
                }
                
                /*
                var newActions = newDialer.GetComponent<PortalDialerController>().actions;
                var oldActions = oldDialer.GetComponent<PortalDialerController>().actions;

                for (var i = 0; i > oldActions.Length; i++)
                {
                    newActions[i] = oldActions[i];
                }

                newDialer.GetComponent<PortalDialerController>().portalSpawnLocation = oldDialer.GetComponent<PortalDialerController>().portalSpawnLocation;
                newDialer.GetComponent<PortalDialerController>().defaultDestination = oldDialer.GetComponent<PortalDialerController>().defaultDestination;
                newDialer.GetComponent<PortalDialerController>().alternateDestinations = oldDialer.GetComponent<PortalDialerController>().alternateDestinations;
                */


                //Destroy island
                foreach (string objectName in islandObjectNames)
                {
                    if (GameObject.Find(objectName) != null)
                    {
                        GameObject.Find(objectName).SetActive(false);
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
