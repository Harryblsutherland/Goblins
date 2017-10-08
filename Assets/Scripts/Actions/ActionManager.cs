using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Current;
    public Button[] Buttons;
    public float autoFireInterval;
    public float autoFireDelay;

    private List<KeyCode> gridKeys = new List<KeyCode>();
    private float[] timer = new float[12];
    private int[] timerTriggerCount = new int[12];
    private Action[] actioncalls = new Action[12];
    void Start()
    {
        gridKeys.Add(KeyCode.Q);
        gridKeys.Add(KeyCode.W);
        gridKeys.Add(KeyCode.E);
        gridKeys.Add(KeyCode.R);
        gridKeys.Add(KeyCode.A);
        gridKeys.Add(KeyCode.S);
        gridKeys.Add(KeyCode.D);
        gridKeys.Add(KeyCode.F);
        gridKeys.Add(KeyCode.Z);
        gridKeys.Add(KeyCode.X);
        gridKeys.Add(KeyCode.C);
        gridKeys.Add(KeyCode.V);
        for (int i = 0; i < Buttons.Length; i++)
        {
            var index = i;
            Buttons[index].onClick.AddListener(delegate ()
            {
                OnButtonCLick(index);
            });


        }
        ClearButton();
    }
    public ActionManager()
    {
        Current = this;
    }
    public void ClearButton()
    {
        foreach (var button in Buttons)
        {
            button.gameObject.SetActive(false);
        }
        for(var i = 0; i < actioncalls.Length; i++)
        {
           actioncalls[i] = () => { };;
        }
    }
    public void AddButton(Sprite Icon, Action onClick, int index)
    {
        //int index = actioncalls.Count;
        
        Buttons[index].gameObject.SetActive(true);
        Buttons[index].GetComponent<Image>().sprite = Icon;
        actioncalls[index] = onClick;
    }
    public void OnButtonCLick(int index)
    {
        actioncalls[index]();
    }
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < gridKeys.Count; i++)
        {
            if (Input.GetKeyDown(gridKeys[i]))
            {
                Current.Buttons[i].onClick.Invoke();
            }
            if (Input.GetKey(gridKeys[i]))
            {
                timer[i] += Time.deltaTime;
                if (timer[i] > autoFireDelay)
                {
                    if (((timer[i] - autoFireDelay) / autoFireInterval) > timerTriggerCount[i])
                    {
                        Current.Buttons[i].onClick.Invoke();
                    }
                }
            }
            if (Input.GetKeyUp(gridKeys[i]))
            {
                timer[i] = 0;
            }
        }
    }
}
