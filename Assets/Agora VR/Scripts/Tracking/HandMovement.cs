/* Agora VR */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu("Agora VR/Tracking/Hand Movement")]
public class HandMovement : MonoBehaviour
{
    private Vector3 lastPosition;
    private float distanceTravelled;
    private float seconds;
    private OVRHand track;

    [SerializeField]
    private GameObject hands;
    [SerializeField]
    private TextMeshPro tmp;

    void Start()
    {
        lastPosition = transform.position;
        distanceTravelled = 0.0f;
        seconds = 0.0f;
        // track = hands.GetComponent<OVRHand>();
    }

    void Update()
    {
        float oldDistance = distanceTravelled;
        // if (track.IsTracked)
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
