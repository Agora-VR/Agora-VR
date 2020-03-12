using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandMovement : MonoBehaviour
{
    private Vector3 lastPosition;
    private float distanceTraveled;
    private float seconds;
    private OVRHand track;

    [SerializeField]
    private GameObject hands;
    [SerializeField]
    private TextMeshPro tmp;

    void Start()
    {
        lastPosition = transform.position;
        distanceTraveled = 0.0f;
        seconds = 0.0f;
        // track = hands.GetComponent<OVRHand>();
    }

    public float getDistTraveled()
    {
        return distanceTraveled;
    }

    public float getHandTime()
    {
        return seconds;
    }

    void Update()
    {
        float oldDistance = distanceTraveled;
        lastPosition = transform.position;


        if (distanceTraveled - oldDistance > 0.01f)
        {
            // if (track.IsTracked)
            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            seconds += Time.deltaTime;
            Debug.Log(seconds);
        }

        tmp.text = "distanceTraveled= " + Mathf.FloorToInt(distanceTraveled) + "\nseconds= " + Mathf.FloorToInt(seconds);
    }
}
