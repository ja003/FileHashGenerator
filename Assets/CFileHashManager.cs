/*
//=========================================
AUTHOR:		Adam Jurík
DATE:		14.02.2018
FUNCTION: 
VZOR:
//=========================================
EXAMPLE:

//=========================================
CHANGE LOG:

//=========================================
TO DO:

//=========================================
*/



using UnityEngine;
using System.Collections.Generic;
using AldaEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DefaultNamespace
{
    [CAssetPath("CFileHashManager")]
    public class CFileHashManager : CScriptableSingleton<CFileHashManager>
    {
        [Header("Files to hash")]
        [SerializeField]
        [Tooltip("Use CFileHashEditor to encrypt names of files you want to hash")]
        public List<string> encryptedNamesOfFilesToHash;
        [Space(2)]

        [Tooltip("Hash of given files will be generated automatically and will be displayed in Log.")]
        [SerializeField]
        public bool generateHashAfterBuild;

#if UNITY_EDITOR
        //=========================================//
        // UNITY METHODS
        //=========================================//

        [MenuItem("AldaEngine/File hash/CFileHashManager %#h", false, 0)]
        private static void SelectSettings()
        {
            Selection.activeObject = Instance;
        }

        [CustomEditor(typeof(CFileHashManager))]
        public class ObjectBuilderEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                this.DrawDefaultInspector();
            }
        }
#endif
        //=========================================//
        // PRIVATE METHODS
        //=========================================//
    }
}
