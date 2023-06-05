using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float zoomSpeed = 2f;
    public float zoomInMax = 1;
    public float zoomOutMax = 15;

    private Camera mainCamera;
    private float startingZPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        startingZPosition = mainCamera.transform.position.z;
    }

    public void zoomScreen (float increment) 
    {
        if(increment == 0) 
        {
            return;
        }
        else
        {
           float target = Mathf.Clamp(mainCamera.orthographicSize + increment, zoomInMax, zoomOutMax); 
           mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, target, Time.deltaTime * zoomSpeed);
        }
    }
}
