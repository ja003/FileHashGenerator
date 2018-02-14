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

#if UNITY_EDITOR

using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CFileHashEditor : EditorWindow
    {
        private string fileName; //(with ext, not full path)
        private string hashedFileName; //use this in 

        [MenuItem("AldaEngine/Show FileHashEditor", false, 1)]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(CFileHashEditor), true, "AldaEngine FileHashEditor");
            window.position = new Rect(200, 200, 600, 600);
        }

        void OnEnable()
        {
        }

        void OnDisable()
        {
        }


        void OnGUI()
        {
            GUILayout.Space(10f);

            GUILayout.BeginVertical();
            GUILayout.Label("New Key", EditorStyles.boldLabel);
            this.fileName = EditorGUILayout.TextField("fileName", this.fileName);

            GUILayout.Space(10f);
            if(GUILayout.Button("hash file name", GUILayout.Width(255)))
            {
                this.hashedFileName = CEncryptionRC4.Encrypt(this.fileName, CFileHashGenerator.CRYPT_KEY);
            }

            GUILayout.Space(10f);
            this.hashedFileName = EditorGUILayout.TextField("hashedFileName", this.hashedFileName);

            GUILayout.EndVertical();
            
            
        }
        
    }
}
#endif
