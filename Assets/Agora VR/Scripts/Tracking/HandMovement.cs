using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandMovement : MonoBehaviour
{
    private Vector3 lastPosition;
    private float distanceTravelled;
    private float seconds;

    [SerializeField]
    private Transform body;
    [SerializeField]
    private TextMeshPro tmp;

    void Start()
    {
        lastPosition = transform.position;
        distanceTravelled = 0.0f;
        seconds = 0.0f;
    }

    void Update()
    {
        float oldDistance = distanceTravelled;
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled - oldDistance > 0.01f)
        {
            seconds += Time.deltaTime;
            Debug.Log(seconds);
        }

        tmp.text = "distanceTraveled= " + Mathf.FloorToInt(distanceTravelled) + "\nseconds= " + Mathf.FloorToInt(seconds);
    }
}
