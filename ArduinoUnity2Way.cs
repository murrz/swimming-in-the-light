using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoUnity2Way : MonoBehaviour
{
    //ARDUINO PORTING
    public static string portName = "/dev/cu.usbserial-0232A4D7";
    public static int portSpeed = 9600;
    private SerialPort sp = new SerialPort(portName, portSpeed);
    private bool state;
    private bool success;
    private string byteValue;
    private float analogValue;

    //SIZE SCALING 
    public float size = 1;
    public float originalScale = 1;
    public float n = 0;
    public Vector3 scaleChange;

    //FLOATING 
    public float waterLevel=4.0f;
    public float bounceDamp=0.5f;
    public float floatHeight=2.0f;
    public Vector3 buoyancyCenterOffset;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;

    //SHIVERING
    public float degreesPerSecond = 15.0f;
    public float amplitude;
    public float frequency = 1f;

    
    //GENERAL
    //public float size; 
    public int size_gain = 2;
    public float float_gain = 1.0f;
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    
    void Awake()
    {
        OpenConnection(); 
    }

    void Start() 
    {
        //Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            //ARDUINO PORTING
            string value = ReadSerialPort();
            print(analogValue);

            //SIZE SCALING 
            n = 500 - analogValue;
            size = (1 - (n / 1000));
            scaleChange = new Vector3(size,size,size);
    
            transform.localScale = originalScale * scaleChange * size_gain;
    
            if (size < 0)
            {
                size = 5+(n/100);
            }

            //FLOATING
            // waterLevel = waterLevel + (size*float_gain);
            // floatHeight = floatHeight + (size*float_gain);
            waterLevel = 7.0f - (analogValue / 30f);
            floatHeight = waterLevel - 0.8f;
            bounceDamp = 0.001f * analogValue;

            actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
            forceFactor = 1.0f - ((actionPoint.y - waterLevel)/floatHeight);

            if(forceFactor > 0.0f)
            {
                upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
                GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
            }

            // // SHIVER
            // // Spin object around Y-Axis
            // transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
    
            // // Float up/down with a Sin()
            // amplitude = 50f / analogValue;
            // frequency = analogValue / 100f;
            // tempPos = posOffset;
            // tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
    
            // transform.position = tempPos;

        }
    }

        
    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                Debug.Log("Closing port, because it's already open");
            }
            else
            {
                sp.Open();
                sp.ReadTimeout = 1;
                Debug.Log("port open at" + portName + portSpeed);
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                Debug.Log("port is already open");
            }
            else
            {
                Debug.Log("port == null");
            }
        }
    }


    public string ReadSerialPort(int timeout = 10)
    {
        string readByte;
        sp.ReadTimeout = timeout;
        //we will try to read values from our serial port
        try
        {
            readByte = sp.ReadLine();
            //print (sp.ReadLine());
            //print(readByte);
            //float analogValue = float.Parse(readByte);
            bool success = float.TryParse(readByte, out analogValue);
            print(analogValue);
            return readByte;
        }
        catch(TimeoutException)
        {
             return null;
        }
    }

}
