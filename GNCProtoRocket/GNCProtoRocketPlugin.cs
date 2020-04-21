using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.AssetPlus;
using R2API.Utils;
using UnityEngine;

namespace GNCProtoRocket
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [R2APISubmoduleDependency(nameof(AssetPlus), nameof(ItemAPI), nameof(ItemDropAPI), nameof(ResourcesAPI))]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class GncProtoRocketPlugin : BaseUnityPlugin
    {
        internal static GameObject BiscoLeashPrefab;
        private const string ModPrefix = "@CustomItem:";
        private const string PrefabPath = ModPrefix + "Assets/Import/belt/belt.prefab";
        private const string IconPath = ModPrefix + "Assets/Import/belt_icon/belt_icon.png";
        
        private const string ModVer = "1.0.0";
        private const string ModName = "GNCProtoRocket";
        public const string ModGuid = "com.GlenCloughley.GNCProtoRocket";
        internal static GncProtoRocketPlugin Instance;

        public void Awake()
        {
            if (Instance == null) { Instance = this; }
            AddAssetBundleProvider();
            GNCProtoRocketConfig.Load();
            GNCProtoRocket.Init();
            Hooks.Init();
        }
        
        private void AddAssetBundleProvider()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GNCProtoRocket.rampage")) 
            {
                
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);

                BiscoLeashPrefab = bundle.LoadAsset<GameObject>("Assets/Import/belt/belt.prefab");
            }
        }
        
    }
}