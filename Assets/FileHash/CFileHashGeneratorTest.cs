/*
//=========================================
AUTHOR:		Adam Jurík
DATE:		13.02.2018
FUNCTION:   Testovací třída funkčnosti CFileHashGenerator.

//=========================================
EXAMPLE:
1. Přidejte tento skript na objekt ve scéně.
2. Po spuštění vygeneruje hash souborů specifikovaných v CFileHashManager.encryptedNamesOfFilesToHash
- vypisuje do logu a zároveň zobrazuje log na canvase
(vygenerování hashe se dá znova vyvolat GUI tlačítekm Refresh)
3. V DLLSpy změňte cokoliv v nějakém sledovaném souboru
4. Po Refresh bude výsledný hash jiný.

//=========================================
*/


using System.Collections;
using UnityEngine;

namespace AldaEngine
{
    public class CFileHashGeneratorTest : MonoBehaviour
    {
        public string myLog;

        public Queue myLogQueue = new Queue();
        
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
            //TODO: check if generated hash matches with hash on server
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

        public int callbackOrder { get; set; }
    }
}
