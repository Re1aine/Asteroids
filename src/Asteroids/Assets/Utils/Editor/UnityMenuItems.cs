using UnityEditor;
using UnityEngine;

namespace Util.Editor
{
    public static class UnityMenuItems
    {
        [MenuItem("Tools/ClearPlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
