using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Agora VR/Misc./Session Manager")]
public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
