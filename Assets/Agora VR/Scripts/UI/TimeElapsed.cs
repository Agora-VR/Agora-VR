using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[AddComponentMenu("Agora VR/UI/Time Elapsed")]
[RequireComponent(typeof(TextMeshPro))]
public class TimeElapsed : MonoBehaviour
{
    private float startTime;
    private float currentTime;
    TimeSpan time;

    private TextMeshPro tmp;

    void Start()
    {
        startTime = 0;
        currentTime = 0;

        tmp = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(Mathf.Floor(currentTime));
        tmp.text = time.ToString(@"hh\:mm\:ss");
    }
}
