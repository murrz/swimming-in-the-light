using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWebcamStream : MonoBehaviour
{
    static WebCamTexture camStream;
    string camName = "HD USB Camera"; // Name of your camera. 
    public Material camMaterial;  // Skybox material

    void Start()
    {

        if (camStream == null)
            camStream = new WebCamTexture(camName, 3840, 2160); // Resolution you want

        if (!camStream.isPlaying)
            camStream.Play();

	if (camMaterial != null)
            camMaterial.mainTexture = camStream;

    }
}
