using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class grassWave : MonoBehaviour
{
    public GameObject grass;
    private double _time;
    public float travel;
    public float spin;

    private void FixedUpdate()
    {
        _time = _time + Time.deltaTime;

        // I assumed that the angle for unity is in degrees.
        // Math.Sin work with Radians. a full wave will take about 6 seconds.
        // It will go between 0 and 90 degrees.
        var angle = (float)(travel + (spin * Math.Sin(_time))); 

        grass.transform.Rotate(transform.rotation.x, transform.rotation.y, angle, Space.Self);
    }
    
    
    
    
    
    
    // public Vector3 eulerAngles1;
    // public Vector3 eulerAngles2;
    // // How long it takes to go from eulerAngles1 to eulerAngles2
    // public float duration;

    // Quaternion rotation1;
    // Quaternion rotation2;

    // private void Start()
    // {
    //     rotation1 = Quaternion.Euler(eulerAngles1);
    //     rotation2 = Quaternion.Euler(eulerAngles2);
    // }

    // private void Update()
    // {
    //     var factor = Mathf.PingPong(Time.time / duration, 1);
    //     // Optionally you can even add some ease-in and -out
    //     factor = Mathf.SmoothStep(0, 1, factor);

    //     // Now interpolate between the two rotations on the current factor
    //     transform.rotation = Quaternion.Slerp(rotation1, rotation2, factor);
    // }
}





// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
