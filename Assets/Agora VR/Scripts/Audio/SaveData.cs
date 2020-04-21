using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using System;
using NAudio;

namespace IBM.Watsson.Examples
{
    public class SaveData : MonoBehaviour
    {
        private string folderPath;

        private const string SAVE_SEPARATOR = "\n";

        [SerializeField]
        private float testTime;

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

        private AudioSource myAudioSource;
        private string microphone;
        // Start is called before the first frame update
        void Start()
        {

            #if UNITY_ANDROID && !UNITY_EDITOR
            folderPath = Application.persistentDataPath;
            #else
            folderPath = Application.dataPath;
            #endif

            headScript = head.GetComponent<HeadMovement>();
            handScriptL = leftHand.GetComponent<HandMovement>();
            handScriptR = rightHand.GetComponent<HandMovement>();
            theTime = System.DateTime.Now.ToString("hh:mm:ss");
            theDate = System.DateTime.Now.ToString("MM/dd/yyyy");
            fileName = System.DateTime.Now.ToString("MM-dd-yyyy_hh-mm-ss");
            speechScript = gameObject.GetComponent<SpeechToText>();



            microphone = Microphone.devices[0].ToString();
            myAudioSource = head.GetComponent<AudioSource>();

            StartCoroutine(TestSave());
        }


        private async void Save()
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
            float score = 1 - (float)Compute(speechString, originalText) / (float)originalText.Length;
            double score2 = 0;

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
            File.WriteAllText(folderPath + "/Save.txt", saveString);

            int length = myAudioSource.clip.samples * myAudioSource.clip.channels;

            float[] data = new float[length];
            myAudioSource.clip.GetData(data, 0);

            float[] finaldata = new float[2 * length];

            for (int i = 0; i < length; i++)
            {
                finaldata[2 * i] = data[i];
                finaldata[2 * i + 1] = data[i];
            }

            AudioClip result = AudioClip.Create("Final", 2 * length, 1, 44100, false);
            result.SetData(finaldata, 0);

            EncodeMP3.convert(result, folderPath + "/test.mp3", 64);

        }
        private IEnumerator TestSave()
        {
            yield return new WaitForSeconds(testTime);

            /*int length = myAudioSource.clip.samples * myAudioSource.clip.channels;

            float[] data = new float[length];
            myAudioSource.clip.GetData(data, 0);

            float[] finaldata = new float[2 * length];

            for (int i = 0; i < length; i++)
            {
                finaldata[2 * i] = data[i];
                finaldata[2 * i + 1] = data[i];
            }

            AudioClip result = AudioClip.Create("Final", 2 * length, 1, 44100, false);
            result.SetData(finaldata, 0);
            */

            //myAudioSource.clip = speechScript.getAudio();

            myAudioSource = head.GetComponent<AudioSource>();
            //myAudioSource = gameObject.GetComponent<AudioSource>();

            //SavWav.Save("myfile2", speechScript.getAudio());
            //EncodeMP3.convert(speechScript.getAudio(), folderPath + "/myfile2.mp3", 64);

            Save();
            //EncodeMP3.convert(result, folderPath + "/myfile3.mp3", 64);
            //myAudioSource.Play();
        }

        // Update is called once per frame
        void Update()
        {
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
    }
}