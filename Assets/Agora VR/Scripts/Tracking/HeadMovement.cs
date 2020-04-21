using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HeadMovement : MonoBehaviour
{
    [SerializeField]
    private float timer;
    private float totalTime;
    private float sphereRadius = 1.0f;
    private float percent;
    private bool value;
    private int counter;

    [SerializeField]
    private TextMeshPro tmp;

    void Start()
    {
        timer = 0.0f;
        totalTime = 0.0f;
        value = false;
        counter = 0;
    }

    public float getHeadTimer()
    {
        return timer;
    }

     public float getTotalTime()
    {
        return totalTime;
    }

    public float getPercent()
    {
        return percent;
    }

    public int getAvoid()
    {
        return counter;
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(Mathf.Floor(totalTime));

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, 1000))
        {
            if (hit.collider.tag == "Seat")
            {
                timer += Time.deltaTime;
                value = true;
            }

            else if (value)
            {
                counter++;
                value = false;
            }
        }

                percent = timer / totalTime;

        tmp.text = "timer= " + Mathf.FloorToInt(timer) + "\ntotalTime= " + time.ToString(@"hh\:mm\:ss");
    }
}
