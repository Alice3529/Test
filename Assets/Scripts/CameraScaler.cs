using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    Camera camera;
    public float sceneWidth = 10;
    [Range(0, 1)] public float value = 0.5f; //match width or height
    void Start()
    {
        camera=GetComponent<Camera>();
    }

    void Update()
    {
        float halfHeight = 0.5f * sceneWidth * Screen.height/Screen.width;
        float newSize=Mathf.Lerp(sceneWidth, halfHeight, value);
        camera.orthographicSize= newSize;
    }
}
