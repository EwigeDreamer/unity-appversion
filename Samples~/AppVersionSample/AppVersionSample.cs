using UnityEngine;
using UnityEngine.UI;

namespace ED.AppVersion.Samples
{
    public class AppVersionSample : MonoBehaviour
    {
        [SerializeField] private Text _versionLabel;
        void Start()
        {
            var data = AppVersionHelper.GetVersionData();
            _versionLabel.text = $"version {data.appVersion}\nbuild {data.buildNumber}";
        }
    }
}