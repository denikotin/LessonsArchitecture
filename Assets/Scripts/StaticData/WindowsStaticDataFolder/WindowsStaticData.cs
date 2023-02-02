using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticData.WindowsStaticDataFolder
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Windows")]
    public class WindowsStaticData : ScriptableObject
    {
       public List<WindowConfig> Configs;
    }
}
