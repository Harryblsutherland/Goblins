using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCradle : MonoBehaviour {

    public float Speed;
    public float Scrollratio;
	// Use this for initialization
	void Start () { 
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * Speed * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //float scrl = Input.GetAxis("Mouse ScrollWheel") * Scrollratio;
            transform.Translate(0, Input.GetAxis("Mouse ScrollWheel") * Scrollratio * -1, 0);
        }
        
	}
}
