/* Agora VR */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[AddComponentMenu("Agora VR/UI/Show Date-Time")]
[RequireComponent(typeof(TextMeshPro))]
public class showDateTime : MonoBehaviour
{
    private TextMeshPro tmp;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        DateTime localDate = DateTime.Now;
        tmp.text = localDate.ToString();
    }
}
