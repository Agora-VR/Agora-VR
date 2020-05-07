using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class AudioVolume : MonoBehaviour
{
    private string folderPath;

    private List<float> listVolume = new List<float>();

    private float highest;
    private float lowest;
    private float timehighest;
    private float timelowest;

    private float time;

    [SerializeField]
    private int testTime;

    private float counter;
    private float[] clipSampleData;
    private float reference;

    private List<string[]> AudioData = new List<string[]>();
    //private List<string[]> ListenData = new List<string[]>();

    [SerializeField]
    private AudioSource audioSource;

    private AudioClip fullAudioClip;

    private AudioClip Combine1()
    {
        AudioClip newClip = audioSource.clip;

        if (newClip == null)
            return fullAudioClip;

        AudioClip fullClip = fullAudioClip;

        int length = Mathf.RoundToInt(newClip.samples * newClip.channels / 2);

        int fullLength = 0;

        if (fullClip != null)
            fullLength = fullClip.samples * fullClip.channels;

        int totalLength = length + fullLength;

        float[] data = new float[totalLength];

        if (fullClip != null)
        {
            float[] buffer1 = new float[fullLength];
            fullClip.GetData(buffer1, 0);
            buffer1.CopyTo(data, 0);
        }

        float[] buffer2 = new float[length];
        newClip.GetData(buffer2, 0);
        buffer2.CopyTo(data, fullLength);

        AudioClip result = AudioClip.Create("Combine", totalLength, 1, 44100, false);
        result.SetData(data, 0);
        return result;
    }

    private AudioClip Combine2()
    {
        AudioClip newClip = audioSource.clip;

        if (newClip == null)
            return fullAudioClip;

        AudioClip fullClip = fullAudioClip;

        int length = Mathf.RoundToInt(newClip.samples * newClip.channels / 2);

        int fullLength = 0;

        if (fullClip != null)
            fullLength = fullClip.samples * fullClip.channels;

        int totalLength = length + fullLength;

        float[] data = new float[totalLength];

        if (fullClip != null)
        {
            float[] buffer1 = new float[fullLength];
            fullClip.GetData(buffer1, 0);
            buffer1.CopyTo(data, 0);
        }

        float[] buffer2 = new float[length];
        newClip.GetData(buffer2, length);
        buffer2.CopyTo(data, fullLength);

        AudioClip result = AudioClip.Create("Combine", totalLength, 1, 44100, false);
        result.SetData(data, 0);
        return result;
    }
    


    private void SaveAudioData(List<string[]> AudioData, string filePath)
    {
        string[][] output = new string[AudioData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = AudioData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            if (index != length - 1)
                sb.AppendLine(string.Join(delimiter, output[index]));
            else
                sb.Append(string.Join(delimiter, output[index]));
        }

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();

        //Debug.Log(filePath);
    }

    private void GetAudioData()
    {
        string[] AudioVolume = new string[3];
        //string[] ListenVolume = new string[3];
        if (time >= counter)
        {
            float volume = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            foreach (var sample in clipSampleData)
            {
                volume += Mathf.Abs(sample);
            }
            volume /= 1024;
            volume = Mathf.Sqrt(volume);


            AudioListener.GetOutputData(clipSampleData, 0);

            float sum = 0f;
            for (int i = 0; i < 1024; i++)
            {
                sum += Mathf.Abs(clipSampleData[i]); // sum squared samples
            }
            float rmsValue = Mathf.Sqrt(sum / 1024); // rms = square root of average

            float micdecibels = 20 * Mathf.Log(volume / reference, 10);
            if (volume == 0.0f)
                micdecibels = 0.0f;


            float listendecibels = 20 * Mathf.Log(rmsValue / reference, 10);
            if (rmsValue == 0.0f)
                listendecibels = 0.0f;
            
            //Debug.Log(volume.ToString());
            AudioVolume[0] = Mathf.RoundToInt(counter * 1000).ToString();
            AudioVolume[1] = micdecibels.ToString();
            AudioVolume[2] = listendecibels.ToString();
            AudioData.Add(AudioVolume);

            counter += 0.1f;
        }
    }

    IEnumerator SaveCSV()
    {
        yield return new WaitForSeconds(testTime);

        SaveAudioData(AudioData, folderPath + "/Audio_Data.csv");
        audioSource.clip = fullAudioClip;
        //gameObject.GetComponent<AudioSource>().clip = fullAudioClip;
        audioSource.Play();
    }

    IEnumerator CombineCoroutine()
    {
        yield return new WaitForSeconds(2);
        if (audioSource.clip == null)
            Debug.Log("STOP COMBINING");

        AudioClip fullAudioClip2;
        while (true)
        {
            yield return new WaitForSeconds(5);
            fullAudioClip2 = Combine1();
            fullAudioClip = fullAudioClip2;

            yield return new WaitForSeconds(5);
            fullAudioClip2 = Combine2();
            fullAudioClip = fullAudioClip2;

            Debug.Log("COMBINING");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            folderPath = Application.persistentDataPath;
            GameObject sessionManager = GameObject.FindWithTag("SessionManager");
            SessionManager sessionScript = sessionManager.GetComponent<SessionManager>();
            testTime = sessionScript.getSessionTime() + 4;
        #else
            folderPath = Application.dataPath;
        #endif

        fullAudioClip = null;

        string[] firstRow = new string[3];
        firstRow[0] = "Timestamp";
        firstRow[1] = "MicDecibels";
        firstRow[2] = "ListenDecibels";
        AudioData.Add(firstRow);

        StartCoroutine(CombineCoroutine());
        //audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        int sampleDataLength = 1024;
        clipSampleData = new float[sampleDataLength];

        time = 0f;
        counter = 0f;
        lowest = 0f;
        highest = 0f; //record events and distracted times
        reference = 0.1f / 1024f;
        audioSource.Play();

        StartCoroutine(SaveCSV());
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.RoundToInt(counter * 1000) <= (testTime - 4)*1000)
            GetAudioData();

            time += Time.deltaTime;
    }
}
