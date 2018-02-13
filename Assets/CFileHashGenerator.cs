/*
//=========================================
AUTHOR:		Adam Jurík
DATE:		13.02.2018
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public static class CFileHashGenerator
    {
        //=========================================//
        // PUBLIC METHODS
        //=========================================//

        public static string GetHashFromFiles(List<string> pFileNames, bool pIsAbsolutePath = false)
        {
            List<string> fullFileNames = pIsAbsolutePath ? pFileNames : GetFullFileNames(pFileNames);
            if(fullFileNames.Count == 0)
            {
                Debug.LogError("No fo files to hash found!");
                return "";
            }
            List<string> hashesFromFiles = GetFilesHashes(fullFileNames);
            string finalHash = "";
            foreach(string h in hashesFromFiles)
            {
                finalHash += h;
            }
            Debug.Log("---------Final hash = " + finalHash);
            return finalHash;
        }

        private static List<string> GetFullFileNames(List<string> pFileNames)
        {
            List<string> fullFileNames = new List<string>();
            foreach(string fileName in pFileNames)
            {
                string searchPattern = "*" + fileName;
                string[] files = Directory.GetFiles(Application.dataPath, searchPattern, SearchOption.AllDirectories);
                if(files.Length == 0)
                {
                    Debug.LogError(fileName + " nor found!");
                }
                else
                {
                    if(files.Length > 1)
                    {
                        Debug.LogError("more than 1 file: " + fileName + " found!");
                    }
                    fullFileNames.Add(files[0]);
                }
            }
            return fullFileNames;
        }

        private static List<string> GetFilesHashes(List<string> pFullFileNames)
        {
            List<string> hashes = new List<string>();
            foreach(string fullFileName in pFullFileNames)
            {
                using(var md5 = MD5.Create())
                {
                    using(var stream = File.OpenRead(fullFileName))
                    {
                        var hash = md5.ComputeHash(stream).ToHex(false);
                        Debug.Log(GetShortName(fullFileName) + " hash = " + hash);
                        hashes.Add(hash);
                    }
                }
            }

            return hashes;
        }

        private static string GetShortName(string pFullFileName)
        {
            string converted = pFullFileName.Replace("\\", "/");
            string[] splited = converted.Split('/');
            return splited[splited.Length - 1];
        }

        //=========================================//
        // PRIVATE METHODS
        //=========================================//  

        public static string ToHex(this byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for(int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}