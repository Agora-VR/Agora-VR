using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadMovement : MonoBehaviour
{
    [SerializeField]
    private Transform audience;
    private float timer;
    private float totaltime;

    [SerializeField]
    private TextMeshPro tmp;

    void Start()
    {
        timer = 0.0f;
        totaltime = 0.0f;
    }

    void Update()
    {
        for (int i = 0; i < audience.transform.childCount; i++)
        {
            GameObject thisObject = audience.transform.GetChild(i).gameObject;
            Vector3 dir = (thisObject.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(dir, transform.forward);
            totaltime += Time.deltaTime;
            if (dot > 0.8f)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                Debug.Log(totaltime);

                tmp.text = "timer= " + Mathf.FloorToInt(timer) + "\ntotalTime= " + Mathf.FloorToInt(totaltime);
                break;
            }
        }
    }
}
