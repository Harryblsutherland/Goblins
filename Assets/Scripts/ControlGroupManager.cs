using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGroupManager : MonoBehaviour
{

    List<List<Interactive>> controlGroups = new List<List<Interactive>>();
    List<KeyCode> numbers = new List<KeyCode>();
    private MouseManager mouseManager;
    private void Start()
    {
        mouseManager = MouseManager.Current;

        numbers.Add(KeyCode.Alpha1);
        numbers.Add(KeyCode.Alpha2);
        numbers.Add(KeyCode.Alpha3);
        numbers.Add(KeyCode.Alpha4);
        numbers.Add(KeyCode.Alpha5);
        numbers.Add(KeyCode.Alpha6);
        numbers.Add(KeyCode.Alpha7);
        numbers.Add(KeyCode.Alpha8);
        numbers.Add(KeyCode.Alpha9);
        numbers.Add(KeyCode.Alpha0);
        foreach (var Num in numbers)
        {
            List<Interactive> controlgroup = new List<Interactive>();
            controlGroups.Add(controlgroup);
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightControl))
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                Debug.Log(numbers[i].ToString());
                if (Input.GetKeyDown(numbers[i]))
                {
                    foreach (var unit in mouseManager.Selections)
                    {
                        controlGroups[i].Add(unit);
                    }
                }
                

            }
        }
        else
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                if (Input.GetKeyDown(numbers[i]))
                {
                    mouseManager.addNewselections(controlGroups[i]);
                }
            }
        }
    }
}
