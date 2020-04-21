using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class AudioVolume : MonoBehaviour
{

    private List<float> listVolume = new List<float>();

    private float highest;
    private float lowest;
    private float timehighest;
    private float timelowest;

    private float time;

    private float counter;
    private float[] clipSampleData;
    private float reference;

    private List<string[]> AudioData = new List<string[]>();
    //private List<string[]> ListenData = new List<string[]>();

    [SerializeField]
    private AudioSource audioSource;

    private AudioClip fullAudioClip;

    /*public static AudioClip Combine1(AudioClip fullClip, AudioClip newClip)
    {
        if (clip1 = null)
            return fullClip;

        float[] data = new float[totalLength];

        int length = (int)(newClip.samples * newClip.channels) / 2;
        int totalLength = length + fullClip.samples * fullClip.channels;


        float[] buffer1 = new float[fullClip.samples * fullClip.channels];
        fullClip.GetData(buffer1, 0);
        buffer.CopyTo(data, totalLength);

        float[] buffer2 = new float[length];
        newClip.GetData(buffer2, 0);
        buffer2.CopyTo(data, totalLength);

        AudioClip result = AudioClip.Create("Combine", length, 1, 88200, false, false);
        result.SetData(data, 0);
        return result;
    }

    public static AudioClip Combine2(AudioClip fullClip, AudioClip newClip)
    {
        if (newClip = null)
            return fullClip;

        float[] data = new float[totalLength];

        int length = (int)(newClip.samples * newClip.channels) / 2;
        int totalLength = length + fullClip.samples * fullClip.channels;


        float[] buffer1 = new float[fullClip.samples * fullClip.channels];
        fullClip.GetData(buffer1, 0);
        buffer1.CopyTo(data, 0);

        float[] buffer2 = new float[length];
        newClip.GetData(buffer2, length);
        buffer2.CopyTo(data, totalLength);

        AudioClip result = AudioClip.Create("Combine", length, 1, 88200, false, false);
        result.SetData(data, 0);
        return result;
    }
    */



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
            sb.AppendLine(string.Join(delimiter, output[index]));

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

            counter += 0.1f;
            //Debug.Log(volume.ToString());
            AudioVolume[0] = Mathf.RoundToInt(counter * 1000).ToString();
            AudioVolume[1] = listendecibels.ToString();
            AudioVolume[2] = micdecibels.ToString();
            AudioData.Add(AudioVolume);
        }
    }

    IEnumerator CombineCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            AudioClip[] allClips = new AudioClip[2] { fullAudioClip, audioSource.clip };
            //AudioClip fullAudioClip2 = Combine(allClips);
            //fullAudioClip = fullAudioClip2;
            Debug.Log("HELLO");
            Debug.Log("HELLO");
            Debug.Log("HELLO");
            Debug.Log("HELLO");
            Debug.Log("HELLO");
            Debug.Log("HELLO");
            Debug.Log("HELLO");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fullAudioClip = null;

        string[] firstRow = new string[3];
        firstRow[0] = "Timestamp";
        firstRow[1] = "Volume";
        firstRow[2] = "Decibels";
        AudioData.Add(firstRow);

        StartCoroutine(CombineCoroutine());
        //audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 20, 88200);
        int sampleDataLength = 1024;
        clipSampleData = new float[sampleDataLength];

        time = 0f;
        counter = 0f;
        lowest = 0f;
        highest = 0f; //record events and distracted times
        reference = 0.1f / 1024f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GetAudioData();

        AudioClip[] allClips = new AudioClip [2] {fullAudioClip, audioSource.clip};

        if (Mathf.RoundToInt(counter) == 22)
        {
            SaveAudioData(AudioData, Application.persistentDataPath + "/Audio_Data.csv");
            counter += 2.0f;
            //audioSource.clip = fullAudioClip;
            gameObject.GetComponent<AudioSource>().clip = fullAudioClip;
            gameObject.GetComponent<AudioSource>().Play();
        }

        time += Time.deltaTime;
    }
}
