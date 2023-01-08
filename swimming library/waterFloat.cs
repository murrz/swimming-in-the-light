using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterFloat : MonoBehaviour
{
    public float waterLevel=4.0f;
    public float bounceDamp=0.5f;
    public float floatHeight=2.0f;
    public Vector3 buoyancyCenterOffset;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
        forceFactor = 1.0f - ((actionPoint.y - waterLevel)/floatHeight);

        if(forceFactor > 0.0f)
        {
            upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
        }
    }
}
