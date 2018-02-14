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


        private int numberOfFilesToHash;
        private string[] filesToHash;
        private string path;
        private string hashedFiles;


        [MenuItem("AldaEngine/File hash/Show FileHashEditor", false, 1)]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(CFileHashEditor), true, "AldaEngine FileHashEditor");
            window.position = new Rect(200, 200, 600, 600);
        }

        void OnEnable()
        {
            //this.filesToHash = new string[this.numberOfFilesToHash];

        }

        void OnDisable()
        {
        }


        void OnGUI()
        {
            GUILayout.Space(10f);

            GUILayout.BeginVertical();
            GUILayout.Label("File name hash", EditorStyles.boldLabel);
            this.fileName = EditorGUILayout.TextField("fileName", this.fileName);
            
            if(GUILayout.Button("hash file name", GUILayout.Width(255)))
            {
                this.hashedFileName = CEncryptionRC4.Encrypt(this.fileName, CFileHashGenerator.CRYPT_KEY);
            }

            this.hashedFileName = EditorGUILayout.TextField("hashedFileName", this.hashedFileName);


            GUILayout.Space(10f);
            GUILayout.Label("Hash of files", EditorStyles.boldLabel);
            
            this.numberOfFilesToHash = EditorGUILayout.IntField("number of files", this.numberOfFilesToHash);
            Array.Resize(ref this.filesToHash, this.numberOfFilesToHash);

            for(int i = 0; i < this.numberOfFilesToHash; i++)
            {
                this.filesToHash[i] = EditorGUILayout.TextField("file " + i, this.filesToHash[i]);

            }
            
            this.path = EditorGUILayout.TextField("path", this.path);
            if(GUILayout.Button("hash files", GUILayout.Width(255)))
            {
                this.hashedFiles = CFileHashGenerator.EncryptAndGetHashFromFiles(this.filesToHash.ToList(), this.path);
            }
            
            this.hashedFiles = EditorGUILayout.TextField("hashedFiles", this.hashedFiles);

            GUILayout.EndVertical();
        }
    }
}
#endif
