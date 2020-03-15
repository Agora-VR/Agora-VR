/* Agora VR */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[AddComponentMenu("Agora VR/UI/Show Battery")]
[RequireComponent(typeof(TextMeshPro))]
public class showBattery : MonoBehaviour
{
    [SerializeField]
    private int updateFrequency = 60;
    private TextMeshPro tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        StartCoroutine(checkBattery());
    }

    IEnumerator checkBattery()
    {
        if (SystemInfo.batteryLevel > 0.6) {
            tmp.text = String.Format("{0:P0}  <sprite=0>", SystemInfo.batteryLevel);
        } else if (SystemInfo.batteryLevel > 0.2) {
            tmp.text = String.Format("{0:P0}  <sprite=1>", SystemInfo.batteryLevel);
        } else {
            tmp.text = String.Format("{0:P0}  <sprite=2>", SystemInfo.batteryLevel);
        }

        // OVRInput.GetControllerBatteryPercentRemaining(OVRInput.Controller.LTouch);

        yield return new WaitForSeconds(updateFrequency);
        StartCoroutine(checkBattery());
    }
}
