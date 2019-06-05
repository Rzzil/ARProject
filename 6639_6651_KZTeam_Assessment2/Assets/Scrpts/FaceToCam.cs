using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCam : MonoBehaviour
{
    GameObject cam;
    void Start()
    {
        cam = Camera.main.gameObject;
    }
    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
