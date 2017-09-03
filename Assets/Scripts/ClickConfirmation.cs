using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickConfirmation : MonoBehaviour
{

    private bool isClickConfirmationOn;
    private GameObject cursor;
    private System.Delegate actionOnClick;
    public static ClickConfirmation current;

    public ClickConfirmation()
    {
        current = this;
    }

    public void StartClickConfirmation(GameObject prCursor, System.Delegate prActionOnClick)
    {
        isClickConfirmationOn = true;
        actionOnClick = prActionOnClick;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isClickConfirmationOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isClickConfirmationOn = false;
                actionOnClick = null;
                cursor = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                actionOnClick.DynamicInvoke();
                isClickConfirmationOn = false;
                actionOnClick = null;
                cursor = null;

            }
        }



    }

}

