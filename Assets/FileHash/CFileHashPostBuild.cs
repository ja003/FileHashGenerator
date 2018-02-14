/*
//=========================================
AUTHOR:		Adam Jurík
DATE:		14.02.2018
FUNCTION:   Třída pro automatické vygenerování hashe souborů specifikovaných v CFileHashManager.

Pro automatické generování je třeba mít zaškrtlé CFileHashManager.generateHashAfterBuild.

Třída je pouze pro automatizaci, výsledný hash se dá zjistit i přes CFileHashEditor, ale 
je třeba to manuálně naklikat.

//=========================================
*/

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

namespace AldaEngine
{
    public class CFileHashPostBuild : IPostprocessBuild
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
        }

        private string GetBuildPath(string pExePath)
        {
            string converted = pExePath.Replace("/", "\\");
            string[] splited = converted.Split('\\');
            string buildPath = converted.Substring(0, converted.Length - splited[splited.Length - 1].Length);
            return buildPath;
        }

        public int callbackOrder { get; set; }
    }
}
#endif