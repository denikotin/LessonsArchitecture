using Assets.Scripts.UI.Services;
using Assets.Scripts.UI.Windows;
using System;

namespace Assets.Scripts.StaticData.WindowsStaticDataFolder
{
    [Serializable]
    public class WindowConfig
    {
        public WindowID WindowID;
        public WindowBase Prefab;
    }
}