/*
//=========================================
AUTHOR:		Adam Jurík
DATE:		13.02.2018
FUNCTION:   Statická třída na vygenerování jednoho hashe pro více souborů.
Stačí zavolat metodu GetHashFromFiles.

//=========================================
*/

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AldaEngine
{
    public static class CFileHashGenerator
    {
        public const string CRYPT_KEY = "FU"; //klíč na šifrování názvů souboru

        //=========================================//
        // PUBLIC METHODS
        //=========================================//

        /// <summary>
        /// Vrátí jeden hash pro zadané soubory.
        /// Výslednou hodnotu je třeba průběžně kontrolovat s hodnotou na serveru.
        /// </summary>
        /// <param name="pPath">Pokud není specifikována cesta, použije se Application.dataPath</param>
        public static string GetHashFromFiles(List<string> pEncryptedFileNames, string pPath = "")
        {
            if(pPath.Length == 0)
            {
                Debug.Log("Path not specified, using Application.dataPath");
                pPath = Application.dataPath;
            }

            List<string> decryptedFileNames = new List<string>();
            foreach(string fn in pEncryptedFileNames)
            {
                string decrypt = CEncryptionRC4.Decrypt(fn, CRYPT_KEY);
                decryptedFileNames.Add(decrypt);
                Debug.Log("Decryption of " + fn + " = " + decrypt);
            }

            List<string> fullFileNames = GetFullFileNames(decryptedFileNames, pPath);
            if(fullFileNames.Count == 0)
            {
                Debug.LogError("No files to hash found!");
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

        /// <summary>
        /// Pouze pro test. Má se používat GetHashFromFiles.
        /// </summary>
        public static string EncryptAndGetHashFromFiles(List<string> pNotEncryptedFileNames, string pPath)
        {
            for(int i = 0; i < pNotEncryptedFileNames.Count; i++)
            {
                pNotEncryptedFileNames[i] = CEncryptionRC4.Encrypt(pNotEncryptedFileNames[i], CFileHashGenerator.CRYPT_KEY);
            }
            return GetHashFromFiles(pNotEncryptedFileNames, pPath);
        }


        //=========================================//
        // PRIVATE METHODS
        //=========================================//  

        /// <summary>
        /// Zkontroluje, zda se soubory nachází v nějaké z podsložek dané cesty.
        /// Pokud ne, zahlásí error a autor by měl zkontrolovat, zda je zadán název souboru správně.
        /// Vrátí absolutní cestu k tomuto souboru.
        /// </summary>
        private static List<string> GetFullFileNames(List<string> pFileNames, string pPath)
        {
            List<string> fullFileNames = new List<string>();
            foreach(string fileName in pFileNames)
            {
                string searchPattern = "*" + fileName;
                string[] files = Directory.GetFiles(pPath, searchPattern, SearchOption.AllDirectories);
                if(files.Length == 0) { Debug.LogError(fileName + " not found!"); }
                else
                {
                    if(files.Length > 1) { Debug.LogError("more than 1 file: " + fileName + " found!"); }
                    fullFileNames.Add(files[0]);
                }
            }
            return fullFileNames;
        }

        /// <summary>
        /// Vrátí hashe daných souborů vygenerované pomocí algoritmu MD5.
        /// </summary>
        private static List<string> GetFilesHashes(List<string> pFullFileNames)
        {
            List<string> hashes = new List<string>();
            foreach(string fullFileName in pFullFileNames)
            {
                using(var md5 = System.Security.Cryptography.MD5.Create())
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

        /// <summary>
        /// Vrátí název souboru (bez celé cesty). Pouze pro přehlednější logy.
        /// </summary>
        private static string GetShortName(string pFullFileName)
        {
            string converted = pFullFileName.Replace("\\", "/");
            string[] splited = converted.Split('/');
            return splited[splited.Length - 1];
        }

        /// <summary>
        /// Převedení bytového pole na string.
        /// </summary>
        public static string ToHex(this byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for(int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}