using System.Linq;
using UnityEditor;
using UnityEngine;
using WildbotLabs.Scriptables.Interfaces;

namespace WildbotLabs.Scriptables.Variables.Editor
{
    [InitializeOnLoad]
    public class AutoResetManager : MonoBehaviour {

        static AutoResetManager()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChangeHandler;
        }

        private static void PlayModeStateChangeHandler(PlayModeStateChange playModeState)
        {
            if (playModeState == PlayModeStateChange.ExitingEditMode || playModeState == PlayModeStateChange.ExitingPlayMode)
            {
                foreach (var resetable in Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IResetable>())
                {
                    resetable.Reset();
                }
            }
        }
    }
}

