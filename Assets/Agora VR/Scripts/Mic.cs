using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Mic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource.loop = true;
        audioSource.clip = Microphone.Start(null, true, 30, 44100);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
