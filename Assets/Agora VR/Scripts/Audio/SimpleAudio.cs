using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudio : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
