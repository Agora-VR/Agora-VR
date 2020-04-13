using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

[AddComponentMenu("Agora VR/Android Plugin/Location Permission")]
public class LocationPermission : MonoBehaviour
{
    void Start()
    {
        checkPermissions();
    }

    private void checkPermissions()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
}
