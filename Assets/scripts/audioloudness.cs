using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioloudness : MonoBehaviour
{
    public int SampleSize=128;
    AudioClip MicClip;  
    // Start is called before the first frame update
    void Start()
    {
        MicrophonetoAudio();
    }


    //function to take audio clip and get loudness 
    public void MicrophonetoAudio()
    {
        //pull the device 
        string MicName=Microphone.devices[0];
        //method to start microphone 
        MicClip=Microphone.Start(MicName,true, 20, AudioSettings.outputSampleRate);

    }

    //function to get audio clip from microphone 
    public float GetLoudnessFromAudio(int clipPosition, AudioClip clip)
    {
        //get an array of data in the dedicated sample size
        int start = clipPosition-SampleSize;
        //case handling start
        if(start<0)
        {
            return 0; 
        }
        float[] AudioData= new float[SampleSize];
        //method that fills array with audio data 
        clip.GetData(AudioData, start);
        //get average loudness 
        float average =0; 
        for(int i=0; i<SampleSize; i++)
        {
            average+=Math.Abs(AudioData[i]);
        }
        //return the average 
        return average/SampleSize;
    }

    //function to get the loudness from the microphone 
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudio(Microphone.GetPosition(Microphone.devices[0]), MicClip);
    }
}
