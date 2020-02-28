using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a basic script that shows the FPS.
/// </summary>

public class showFPS : MonoBehaviour {

    private TextMeshPro fpsText;
    private float deltaTime;

    void Awake()
    {
        fpsText = this.gameObject.GetComponent<TextMeshPro>();
    }

    void Update ()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil (fps).ToString ();
    }
}