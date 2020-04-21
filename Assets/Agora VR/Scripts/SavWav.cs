using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Yeti.MMedia;
using Yeti.MMedia.Mp3;
using WaveLib;
using CSAudioConverter;
using CSCore;
using CSCore.Ffmpeg;


    public static class SavWav
{
        const int HEADER_SIZE = 44;

    public static bool Save(string filename, AudioClip clip)
    {
        if (!filename.ToLower().EndsWith(".wav"))
        {
            filename += ".wav";
        }

        var filepath = Path.Combine(Application.dataPath, filename);

        Debug.Log(filepath);

        // Make sure directory exists if user is saving to sub dir.
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));

        using (var fileStream = CreateEmpty(filepath))
        {

            ConvertAndWrite(fileStream, clip);

            WriteHeader(fileStream, clip);

        }

        /*AudioConverter audioConverter1 = new AudioConverter();

        //Init the component
        audioConverter1.UserName = "Free@Usage";
        audioConverter1.UserKey = "d1200cee3a2f9f7aeddb37f9ea398592";

        audioConverter1.DestinatioFile = Path.Combine(Application.dataPath, "myfile2.mp3"); ;
        //Set the destination audio format, this can be AAC, APE, MP2, MP3, OGG, PCM WAV, ACM WAV and WMA
        audioConverter1.Format = CSAudioConverter.Format.MP3;

        //Add the WAV file to the SourceFiles list
        TimeSpan from_time = new TimeSpan(0, 0, 1);
        TimeSpan to_time = new TimeSpan(0, 0, 9);
        Options.Core.SourceFile sourceFile =
            new Options.Core.SourceFile(filename, from_time, to_time);

        audioConverter1.SourceFiles.Add(sourceFile);

        //Events:

        //On progress event

        audioConverter1.ConvertProgress += (s, e) =>
        {
            Console.WriteLine("\nProcessing " + audioConverter1.SourceFiles[e.Track].File);
            Console.WriteLine("\nProgress " + e);
        };

        //When the process has done
        audioConverter1.ConvertDone += (s,e) =>
        {
            Console.WriteLine("\nDone.");
        };

        //Run the process
        audioConverter1.Convert();

    */
        WaveStream InStr = new WaveStream("myfile2.wav");
        try
        {
            Mp3Writer writer = new Mp3Writer(new FileStream("SomeFile.mp3", FileMode.Create), InStr.Format);
            try
            {
                byte[] buff = new byte[writer.OptimalBufferSize];
                int read = 0;
                while ((read = InStr.Read(buff, 0, buff.Length)) > 0)
                {
                    //writer.Write(buff, 0, read);
                }
            }
            finally
            {
                //writer.Close();
            }
        }
        finally
        {
            InStr.Close();
        }
        return true;
    }

    public static AudioClip TrimSilence(AudioClip clip, float min)
    {
        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        return TrimSilence(new List<float>(samples), min, clip.channels, clip.frequency);
    }

    public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz)
    {
        return TrimSilence(samples, min, channels, hz, false, false);
    }

    public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream)
    {
        int i;

        for (i = 0; i < samples.Count; i++)
        {
            if (Mathf.Abs(samples[i]) > min)
            {
                break;
            }
        }

        samples.RemoveRange(0, i);

        for (i = samples.Count - 1; i > 0; i--)
        {
            if (Mathf.Abs(samples[i]) > min)
            {
                break;
            }
        }

        samples.RemoveRange(i, samples.Count - i);

        var clip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);

        clip.SetData(samples.ToArray(), 0);

        return clip;
    }

    static FileStream CreateEmpty(string filepath)
    {
        var fileStream = new FileStream(filepath, FileMode.Create);
        byte emptyByte = new byte();

        for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
        {
            fileStream.WriteByte(emptyByte);
        }

        return fileStream;
    }

    static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
    {

        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        Int16[] intData = new Int16[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

        Byte[] bytesData = new Byte[samples.Length * 2];
        //bytesData array is twice the size of
        //dataSource array because a float converted in Int16 is 2 bytes.

        int rescaleFactor = 32767; //to convert float to Int16

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        fileStream.Write(bytesData, 0, bytesData.Length);
    }

    static void WriteHeader(FileStream fileStream, AudioClip clip)
    {

        var hz = clip.frequency;
        var channels = clip.channels;
        var samples = clip.samples;

        fileStream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
        fileStream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        fileStream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        fileStream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        fileStream.Write(subChunk1, 0, 4);

        UInt16 two = 2;
        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        fileStream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        fileStream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        fileStream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
        fileStream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        fileStream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        fileStream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        fileStream.Write(subChunk2, 0, 4);

        //		fileStream.Close();
    }
}
