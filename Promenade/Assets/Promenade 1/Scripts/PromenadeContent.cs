using HG;
using R2API;
using RoR2;
using RoR2.ContentManagement;
using RoR2.ExpansionManagement;
using RoR2.Networking;
using RoR2BepInExPack.GameAssetPaths;
using ShaderSwapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using static RoR2.Console;
using static UnityEngine.UI.Image;

namespace Promenade.Content
{
    public static class PromenadeContent
    {

        internal const string ScenesAssetBundleFileName = "ObservatoryScene";
        internal const string AssetsAssetBundleFileName = "ObservatoryAssets";

        private static AssetBundle _scenesAssetBundle;
        private static AssetBundle _assetsAssetBundle;

        internal static UnlockableDef[] UnlockableDefs;
        internal static SceneDef[] SceneDefs;

        //ancientobservatory
        internal static SceneDef PromenadeSceneDef;
        internal static Sprite PromenadeSceneDefPreviewSprite;
        internal static Material PromenadeBazaarSeer;
        //itancientobservatory
        internal static SceneDef SimuSceneDef;
        internal static Sprite SimuSceneDefPreviewSprite;
        internal static Material SimuBazaarSeer;
        // metal material for artifact portal
        internal static Material MetalMaterial;

        public static List<Material> SwappedMaterials = new List<Material>();

        internal static IEnumerator LoadAssetBundlesAsync(AssetBundle scenesAssetBundle, AssetBundle assetsAssetBundle, IProgress<float> progress, ContentPack contentPack)
        {
            _scenesAssetBundle = scenesAssetBundle;
            _assetsAssetBundle = assetsAssetBundle;
            
            var upgradeStubbedShaders = _assetsAssetBundle.UpgradeStubbedShadersAsync();
            while (upgradeStubbedShaders.MoveNext())
            {
                yield return upgradeStubbedShaders.Current;
            }
            
            yield return LoadAllAssetsAsync(assetsAssetBundle, progress, (Action<UnlockableDef[]>)((assets) =>
            {
                contentPack.unlockableDefs.Add(assets);
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<Material[]>)((assets) =>
            {
                MetalMaterial = assets.First(a => a.name == "matPRMMetal");
            }));

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<Sprite[]>)((assets) =>
            {
                PromenadeSceneDefPreviewSprite = assets.First(a => a.name == "texAOScenePreview");
                SimuSceneDefPreviewSprite = assets.First(a => a.name == "texAOScenePreview");
            }));
            

            yield return LoadAllAssetsAsync(_assetsAssetBundle, progress, (Action<SceneDef[]>)((assets) =>
            {
                SceneDefs = assets;
                PromenadeSceneDef = SceneDefs.First(sd => sd.baseSceneNameOverride == "observatory_wormsworms");
                SimuSceneDef = SceneDefs.First(sd => sd.baseSceneNameOverride == "itobservatory_wormsworms");
                Log.Debug(PromenadeSceneDef.nameToken);
                Log.Debug(SimuSceneDef.nameToken);
                contentPack.sceneDefs.Add(assets);
            }));

            PromenadeSceneDef.portalMaterial = R2API.StageRegistration.MakeBazaarSeerMaterial((Texture2D)PromenadeSceneDef.previewTexture);

            var mainTrackDefRequest = Addressables.LoadAssetAsync<MusicTrackDef>("RoR2/Base/Common/MusicTrackDefs/muSong14.asset");
            while (!mainTrackDefRequest.IsDone)
            {
                yield return null;
            }
            var simuTrackDefRequest = Addressables.LoadAssetAsync<MusicTrackDef>("RoR2/Base/Common/MusicTrackDefs/muSong08.asset");
            while (!mainTrackDefRequest.IsDone)
            {
                yield return null;
            }
            var bossTrackDefRequest = Addressables.LoadAssetAsync<MusicTrackDef>("RoR2/Base/Common/MusicTrackDefs/muSong23.asset");
            while (!bossTrackDefRequest.IsDone)
            {
                yield return null;
            }
            PromenadeSceneDef.mainTrack = mainTrackDefRequest.Result;
            PromenadeSceneDef.bossTrack = bossTrackDefRequest.Result;
            SimuSceneDef.mainTrack = simuTrackDefRequest.Result;
            SimuSceneDef.bossTrack = bossTrackDefRequest.Result;

            if (Promenade.enableRegular.Value)
            {
                R2API.StageRegistration.RegisterSceneDefToNormalProgression(PromenadeSceneDef);
            }
            if (Promenade.enableSimulacrum.Value && Promenade.stage1Simulacrum.Value)
            {
                Simulacrum.RegisterSceneToSimulacrum(SimuSceneDef);
            } else if (Promenade.enableSimulacrum.Value && !Promenade.stage1Simulacrum.Value)
            {
                Simulacrum.RegisterSceneToSimulacrum(SimuSceneDef, false);
            }
        }

internal static void Unload()
        {
            _assetsAssetBundle.Unload(true);
            _scenesAssetBundle.Unload(true);
        }

        private static IEnumerator LoadAllAssetsAsync<T>(AssetBundle assetBundle, IProgress<float> progress, Action<T[]> onAssetsLoaded) where T : UnityEngine.Object
        {
            var sceneDefsRequest = assetBundle.LoadAllAssetsAsync<T>();
            while (!sceneDefsRequest.isDone)
            {
                progress.Report(sceneDefsRequest.progress);
                yield return null;
            }

            onAssetsLoaded(sceneDefsRequest.allAssets.Cast<T>().ToArray());

            yield break;
        }
    }
}
