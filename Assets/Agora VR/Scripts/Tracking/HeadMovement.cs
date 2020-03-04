using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadMovement : MonoBehaviour
{
    [SerializeField]
    private float timer;
    private float totalTime;
    private float sphereRadius = 2.0f;

    [SerializeField]
    private TextMeshPro tmp;

    void Start()
    {
        timer = 0.0f;
        totalTime = 0.0f;
    }

    void Update()
    {

        RaycastHit hit;

        if(Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, 1000))
            if(hit.collider.tag == "Seat")
                timer += Time.deltaTime;

        tmp.text = "timer= " + Mathf.FloorToInt(timer) + "\ntotalTime= " + Mathf.FloorToInt(totalTime);
    }
}
