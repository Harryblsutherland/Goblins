using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCradle : MonoBehaviour
{

    public float Speed;
    public float Scrollratio;
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
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * Speed * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //float scrl = Input.GetAxis("Mouse ScrollWheel") * Scrollratio;
            transform.Translate(0, Input.GetAxis("Mouse ScrollWheel") * Scrollratio * -1, 0);
        }

    }
}