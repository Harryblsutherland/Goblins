using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocationmanager : MonoBehaviour
{


    List<Vector3> locations = new List<Vector3>();
    List<KeyCode> numbers = new List<KeyCode>();
    private bool latestart = true;
    private void Start()
    {
        numbers.Add(KeyCode.F1);
        numbers.Add(KeyCode.F2);
        numbers.Add(KeyCode.F3);
        numbers.Add(KeyCode.F4);
        numbers.Add(KeyCode.F5);
        numbers.Add(KeyCode.F6);
        numbers.Add(KeyCode.F7);
        numbers.Add(KeyCode.F8);
        foreach (var Num in numbers)
        {
            locations.Add(new Vector3());
        }
        foreach (var player in RtsManager.Current.Players)
        {
            if (player.IsAi)
            {
                continue;
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                var pos = player.Location.position;
                pos.y = 80;
                pos.z -= 50;
                locations[i] = pos;
            }
        }



    }
    // Update is called once per frame
    void Update()
    {
     
     
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightControl))
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                if (Input.GetKeyDown(numbers[i]))
                {
                    locations[i] = Camera.main.transform.position;
                }
            }
        }
        else
        { 
            for (var i = 0; i < numbers.Count; i++)
            {
                if (Input.GetKeyDown(numbers[i]))
                {
                    Camera.main.transform.position = locations[i];
                   // Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, locations[i], 9999 * Time.deltaTime);
                }
            }
        }
    }
}
