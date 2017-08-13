using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCradle : MonoBehaviour
{

    public float scrollspeed = 40;
    public float scrollratio = 40;
    public float borderbuffer = 25;
    private float heightRayLength;

    // Use this for initialization
    void Start()
    {
        foreach ( var player in RtsManager.Current.Players)
        {
            if (player.IsAi)
            {
                continue;
            }

            var pos = player.Location.position;
            pos.y = 80;
            pos.z -= 50;

            transform.position = pos;
        }
    }
    // Update is called once per frame
    public float GetGroundHeight()
    {
        RaycastHit hit;
        int layerMask = 1 << 8; //bit shift the index of the 8th layer to get its bitmask so only terrain is considered the ground

        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, heightRayLength, layerMask))
        {
            return hit.point.y + 1;
        }
        else if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, heightRayLength, layerMask))
        {
            return hit.point.y + 1;
        }

        //No hit? What the hell happened?! Throw an exception!
        throw new UnityException("Camera could not find any ground beneath it.");
    }
    void Update()
    {

        transform.Translate(Input.GetAxis("Horizontal") * scrollspeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * scrollspeed * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //float scrl = Input.GetAxis("Mouse ScrollWheel") * Scrollratio;
            transform.Translate(0, Input.GetAxis("Mouse ScrollWheel") * scrollratio * -1, 0);
        }
        if (Input.mousePosition.x > Screen.width - borderbuffer)
        {
            transform.Translate(Vector3.right * scrollspeed * Time.deltaTime);
        }
        if (Input.mousePosition.x < borderbuffer)
        {
            transform.Translate(Vector3.left * scrollspeed * Time.deltaTime);
        }
        if (Input.mousePosition.y < borderbuffer)
        {
            transform.Translate(Vector3.back * scrollspeed * Time.deltaTime);
        }
        if (Input.mousePosition.y > Screen.height - borderbuffer)
        {
            transform.Translate(Vector3.forward * scrollspeed * Time.deltaTime);
        }
        
    

    }
}