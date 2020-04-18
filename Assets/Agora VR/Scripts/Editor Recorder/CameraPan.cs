using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    public dir direction = dir.pX;

    private Vector3 vector;

    public enum dir {
        pY,
        nY,
        pX,
        nX,
        pZ,
        nZ
    };

    void Awake()
    {
        switch (direction) {
            case dir.pY: vector = Vector3.up;       break;
            case dir.nY: vector = Vector3.down;     break;
            case dir.pX: vector = Vector3.right;    break;
            case dir.nX: vector = Vector3.left;     break;
            case dir.pZ: vector = Vector3.forward;  break;
            case dir.nZ: vector = Vector3.back;     break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(vector * Time.deltaTime * speed, Space.World);
        transform.LookAt(target);
    }
}
