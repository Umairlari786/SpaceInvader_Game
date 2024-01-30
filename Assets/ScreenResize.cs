using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResize : MonoBehaviour
{
    public int targetWidth = 677;
    public float pixelsToUnit = 100;
   
    // Update is called once per frame
    void Update()
    {
        int width = Screen.width;
        int height = Screen.height;
        float aspectRation = width / height;
        float aspectGation = width / height;
        /*
        int height = Mathf.RoundToInt(targetWidth /(float) Screen.width * Screen.height);
        Camera cam = Camera.main;
        cam.orthographicSize = height/pixelsToUnit/2;
        //float SW = Screen.width;
        //float SH = Screen.height;
        int x = 1;*/
    }
}
