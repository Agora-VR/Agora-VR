using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace IBM.Watsson.Examples
{
    public class SaveData : MonoBehaviour
    {
        private const string SAVE_SEPARATOR = "\n";

        private HeadMovement headScript;
        [SerializeField]
        private GameObject head;

        private HandMovement handScriptL;
        [SerializeField]
        private GameObject leftHand;

        private HandMovement handScriptR;
        [SerializeField]
        private GameObject rightHand;

        private SpeechToText speechScript;
        private string theTime;
        private string theDate;
        string fileName;
        int i = 1;

        // Start is called before the first frame update
        void Start()
        {
            headScript = head.GetComponent<HeadMovement>();
            handScriptL = leftHand.GetComponent<HandMovement>();
            handScriptR = rightHand.GetComponent<HandMovement>();
            theTime = System.DateTime.Now.ToString("hh:mm:ss");
            theDate = System.DateTime.Now.ToString("MM/dd/yyyy");
            fileName = System.DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss");
            speechScript = gameObject.GetComponent<SpeechToText>();
        }

        private void Save()
        {
            float headTimer = headScript.getHeadTimer();
            float totalTime = headScript.getTotalTime();
            float handTimeL = handScriptL.getHandTime();
            float handTimeR = handScriptR.getHandTime();
            float handDistL = handScriptL.getDistTraveled();
            float handDistR = handScriptR.getDistTraveled();
            float percent = headScript.getPercent();
            float avoid = headScript.getAvoid();
            string speechString = speechScript.getFinalText();
            string originalText = "our project focuses on the treatment of social anxiety and social phobias by utilizing VR technology for psychological and cognitive therapy";
            float score = 1-(float)Compute(speechString,originalText)/(float)originalText.Length;
            double score2 = CalculateSimilarity(speechString, originalText);

            string[] contents = new string[]
            {
            "Session Start Date: "+theDate,
            "Session Start Time: "+theTime,
            "Eye Contact Timer: "+headTimer,
            "Eye Contact Percentage: "+percent,
            "Total Time: "+totalTime,
            "Left Hand Timer: "+handTimeL,
            "Right Hand Timer: "+handTimeR,
            "Left Hand Distance: "+handDistL,
            "Right Hand Distance: "+handDistR,
            "Number of times avoiding contact: "+avoid,
            "Speech: "+speechString,
            "Score: "+score,
            "Score2: "+score2
            };
           
            string saveString = string.Join(SAVE_SEPARATOR, contents);
            File.WriteAllText(Application.dataPath + "/save_" + fileName + ".text", saveString);

        }

        // Update is called once per frame
        void Update()
        {
            if (headScript.getTotalTime() >= 20.0f && i == 1)
            {
                Save();
                i++;
            }
        }

        public static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }
        int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

    }
}