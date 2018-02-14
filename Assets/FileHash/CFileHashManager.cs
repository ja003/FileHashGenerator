/*
//=========================================
AUTHOR:		Adam Jurík
DATE:		14.02.2018
FUNCTION:   Scriptable object, na kterém se definuje, jaké soubory budou podléhat hashovací kontrole.

Soubory se specifikují jako zakryptovaný název soubory (i s příponou). Pro vygenerování zakritovaného jména 
souboru použijte CFileHashEditor.

Soubory jsou uloženy zakriptovaně, aby hacker nenašel spojitost tak lehce.

//=========================================
EXAMPLE:
příklad hashe souboru:
encryptedNamesOfFilesToHash.element0 = nHJ3hoyq1TVAH+3veEYf3tM2xtMgpv4+YVjjx4uOkcqAc/XFOXs=

//=========================================
*/

using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AldaEngine
{
    [CAssetPath("CFileHashManager")]
    public class CFileHashManager : CScriptableSingleton<CFileHashManager>
    {
        [Header("Files to hash")]
        [SerializeField]
        [Tooltip("Pro získání zakryptovaného názvu souboru použijte CFileHashEditor.")]
        public List<string> encryptedNamesOfFilesToHash;
        [Space(2)]
        
        [Tooltip("Po dokončení buildu bude hash vygenerován automaticky a vypíše se do Logu.")]
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
