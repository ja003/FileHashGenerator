using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace DefaultNamespace
{
    public class CFileHashGeneratorTest : MonoBehaviour
    {
        public string myLog;

        public Queue myLogQueue = new Queue();

        [SerializeField]
        private List<string> fileNames;

        [SerializeField]
        private List<string> encryptedFileNames;

        public void Start()
        {
            //this.StartCoroutine(this.AutoRefresh());
            this.Refresh();
        }

        private IEnumerator AutoRefresh()
        {
            while(true)
            {
                this.Refresh();
                yield return new WaitForSeconds(2);
            }
        }
        
        private void Refresh()
        {
            //List<string> fileNames = new List<string>() { "CFileHashGenerator.cs", "Assembly-CSharp.dll" };

            string hash = CFileHashGenerator.GetHashFromFiles(CFileHashManager.Instance.encryptedNamesOfFilesToHash);
            if(hash != CFileHashManager.Instance.finalHash)
            {
                Debug.LogError("Hashes dont match!");
            }
            else
            {
                Debug.Log("Hashes match");
            }

            /*List<string> _encryptedFileNames = new List<string>();
            foreach(string fn in this.fileNames)
            {
                string encryptedFn = CEncryptionRC4.Encrypt(fn, CFileHashGenerator.CRYPT_KEY);
                _encryptedFileNames.Add(encryptedFn);
                Debug.Log("Encryption of filename " + fn + " = " + encryptedFn);
            }

            Debug.Log("Hash of given files");
            CFileHashGenerator.GetHashFromFiles(_encryptedFileNames, Application.dataPath);

            Debug.Log("Hash of given encryptedFiles");
            CFileHashGenerator.GetHashFromFiles(this.encryptedFileNames, Application.dataPath);*/

        }

        public void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        public void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        public void HandleLog(string logString, string stackTrace, LogType type)
        {
            myLog = logString;
            string newString = "\n [" + type + "] : " + myLog;
            myLogQueue.Enqueue(newString);
            if(type == LogType.Exception)
            {
                newString = "\n" + stackTrace;
                myLogQueue.Enqueue(newString);
            }
            myLog = string.Empty;
            foreach(string mylog in myLogQueue)
            {
                myLog += mylog;
            }
        }

        public void OnGUI()
        {
            if(GUI.Button(new Rect(0, 0, 100, 20), "REFRESH"))
            {
                this.Refresh();
            }

            GUILayout.Label(myLog);
        }

        public int callbackOrder { get; private set; }


    }
}
