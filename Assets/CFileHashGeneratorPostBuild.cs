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
            Debug.Log("OnPostprocessBuild " + path);

            CFileHashGenerator.EncryptAndGetHashFromFiles(
                new List<string>()
                    {
                        "Assembly-CSharp.dll"
                    },
                "C:\\Users\\Silentium-2\\Desktop\\unity_try\\FileHash_try\\Builds");// path);
        }

        public int callbackOrder { get; private set; }
    }
}
#endif