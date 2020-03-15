/* Agora VR */

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Agora VR/UI/Show FPS")]
[RequireComponent(typeof(TextMeshPro))]
/// <summary>
/// This is a basic script that shows the FPS.
/// </summary>

public class showFPS : MonoBehaviour {

    private TextMeshPro tmp;
    private float deltaTime;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    void Update ()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        tmp.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }
}