using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu("Agora VR/UI/Heartrate")]
[RequireComponent(typeof(TextMeshPro))]
public class Heartrate : MonoBehaviour
{
    // Currently a false heartrate for placeholder purposes.

    private float heartrate = 0.0f;
    private TextMeshPro tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        StartCoroutine(NextBPM());
    }

    private IEnumerator NextBPM()
    {
        yield return new WaitForSeconds(5);
        tmp.text = Random.Range(60,101).ToString() + " BPM";

        StartCoroutine(NextBPM());
    }
}
