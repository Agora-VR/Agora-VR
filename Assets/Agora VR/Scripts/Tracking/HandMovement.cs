using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    Vector3 lastPosition;
    float distanceTravelled;
    float seconds;
    public Transform body;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        distanceTravelled = 0.0f;
        seconds = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float oldDistance = distanceTravelled;
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled - oldDistance > 0.02f)
        {
            seconds += Time.deltaTime;
            Debug.Log(seconds);
        }
    }
}
