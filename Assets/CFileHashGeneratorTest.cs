using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class CFileHashGeneratorTest : MonoBehaviour
    {

        public string myLog;

        public Queue myLogQueue = new Queue();

        public void Start()
        {
            CFileHashGenerator.GetHashFromFiles(new List<string>() { "CFileHashGenerator.cs" });
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
            GUILayout.Label(myLog);
        }
    }
}
