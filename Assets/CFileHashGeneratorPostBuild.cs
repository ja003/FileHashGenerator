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

            CFileHashGenerator.GetHashFromFiles(
                new List<string>()
                {
                    "CFileHashGenerator.cs",
                    "Assembly-CSharp.dll"
                });
        }

        public int callbackOrder { get; private set; }
    }
}
#endif