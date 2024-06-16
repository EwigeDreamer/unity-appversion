using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ED.AppVersion
{
    public static class AppVersionHelper
    {
#if !UNITY_EDITOR
		private const string assetName = "app_version";
#endif
		
        public static AppVersionData GetVersionData()
        {
            AppVersionData data;
			
#if UNITY_EDITOR
            data.appVersion = Application.version;
            data.buildNumber = GetBuildNumber();
#else
			var asset = Resources.Load<TextAsset>(assetName);
			if (asset == null) throw new InvalidOperationException($"{nameof(AppVersionHelper)}.{nameof(GetVersionData)}: {nameof(TextAsset)} {assetName} not found");
			data = JsonUtility.FromJson<AppVersionData>(asset.text);
#endif
            return data;
        }
		
#if UNITY_EDITOR
        private static string GetBuildNumber()
        {
            return EditorUserBuildSettings.activeBuildTarget switch
            {
                BuildTarget.iOS => PlayerSettings.iOS.buildNumber,
                BuildTarget.Android => PlayerSettings.Android.bundleVersionCode.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(Application.platform))
            };
        }
#endif
    }
}