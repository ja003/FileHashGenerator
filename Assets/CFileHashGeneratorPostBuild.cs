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
            if(!CFileHashManager.Exists) { return; }
            if(!CFileHashManager.Instance.generateHashAfterBuild) { return; }

            string buildPath = this.GetBuildPath(path);
            Debug.Log("OnPostprocessBuild " + buildPath);

            CFileHashGenerator.GetHashFromFiles(
                CFileHashManager.Instance.encryptedNamesOfFilesToHash,
                buildPath);

            //CFileHashGenerator.EncryptAndGetHashFromFiles(
            //    new List<string>
            //        {
            //            "Assembly-CSharp.dll"
            //        },
            //    buildPath);
        }

        private string GetBuildPath(string pExePath)
        {
            string converted = pExePath.Replace("/", "\\");
            string[] splited = converted.Split('\\');
            string buildPath = converted.Substring(0, converted.Length - splited[splited.Length - 1].Length);
            return buildPath;
        }

        public int callbackOrder { get; private set; }
    }
}
#endif