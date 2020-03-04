using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public Transform audeince;
    public float timer;
    public float totaltime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        totaltime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < audeince.transform.childCount; i++)
        {
            GameObject thisObject = audeince.transform.GetChild(i).gameObject;
            Vector3 dir = (thisObject.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(dir, transform.forward);
            totaltime += Time.deltaTime;
            if (dot > 0.9f)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                Debug.Log(totaltime);
                break;
            }
        }
    }
}
