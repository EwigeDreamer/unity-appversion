using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace EwigeDreamer.AppVersion.Editor
{
    public class ApplicationVersionBuildPreprocessor : IPreprocessBuildWithReport
    {
        private const string assets = "Assets";
        private const string resources = "Resources";
        private const string file = "app_version";

        private static readonly string filePath = Path.Combine(assets, resources, $"{file}.txt");
			
		
        public int callbackOrder => int.MaxValue;
        public void OnPreprocessBuild(BuildReport report)
        {
            AppVersionData data;
            data.appVersion = Application.version;
            data.buildNumber = GetBuildNumber();
            var json = JsonUtility.ToJson(data, true);

            File.WriteAllText(filePath, json);
            AssetDatabase.ImportAsset(filePath);
        }

        private static string GetBuildNumber()
        {
            return EditorUserBuildSettings.activeBuildTarget switch
            {
                BuildTarget.iOS => PlayerSettings.iOS.buildNumber,
                BuildTarget.Android => PlayerSettings.Android.bundleVersionCode.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(Application.platform))
            };
        }
    }
}