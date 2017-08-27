using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCradle : MonoBehaviour
{

    public float scrollspeed = 40;
    public float scrollratio = 40;
    public float borderbuffer = 25;
    public float maxHeight = 50;
    public float minHeight = 25;
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
            pos.y = maxHeight;
            pos.z -= 50;

            transform.position = pos;
        }
    }
  
    void Update()
    {
        var pos = transform.position;
        transform.Translate(Input.GetAxis("Horizontal") * scrollspeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * scrollspeed * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //float scrl = Input.GetAxis("Mouse ScrollWheel") * Scrollratio;
            transform.Translate(0, Input.GetAxis("Mouse ScrollWheel") * scrollratio * -1, 0);
            pos = transform.position;
            if (transform.position.y > maxHeight)
            {
                
                pos.y = maxHeight;
                transform.position = pos;
            }
            if (transform.position.y < minHeight)
            {

                pos.y = minHeight;
                transform.position = pos;
            }

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
        pos = transform.position;
        if (transform.position.z < -190)
        {
            pos.z = -190f;
            transform.position = pos;
        }


    }
}