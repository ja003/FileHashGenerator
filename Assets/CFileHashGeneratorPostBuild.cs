#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

namespace DefaultNamespace
{
    public class CFileHashGeneratorPostBuild : IPostprocessBuild
    {
        public void OnPostprocessBuild(BuildTarget target, string path)
        {

            string converted = path.Replace("/", "\\");
            string[] splited = converted.Split('\\');
            string buildPath = converted.Substring(0, converted.Length - splited[splited.Length - 1].Length);
            /*for(int i = 0; i < splited.Length - 1; i++)
            {
                buildPath += splited[i] + "\\";
            }*/

            Debug.Log("OnPostprocessBuild " + buildPath);


            CFileHashGenerator.EncryptAndGetHashFromFiles(
                new List<string>()
                    {
                        "Assembly-CSharp.dll"
                    },
                buildPath);
        }

        public int callbackOrder { get; private set; }
    }
}
#endif