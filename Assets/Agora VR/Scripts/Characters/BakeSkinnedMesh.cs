using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeSkinnedMesh : MonoBehaviour
{
    void Start()
    {
        SkinnedMeshRenderer[] children = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer child in children)
        {
            Mesh bakedMesh = new Mesh();
            child.BakeMesh(bakedMesh);
        }
    }
}
