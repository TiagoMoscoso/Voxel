using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    public Color voxelColor = Color.white;
    public void Start()
    {
        GetComponent<Renderer>().material.color = voxelColor;
    }


    public void SetColor(Color color)
    {
        voxelColor = color;
        GetComponent<Renderer>().material.color = voxelColor;
    }
}
