using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Current;
    public Button[] Buttons;
    private List<Action> actioncalls = new List<Action>();

    public ActionManager()
    {
        Current = this;
    }
    public void ClearButton()
    {
        foreach (var b in Buttons)
            b.gameObject.SetActive(false);
        actioncalls.Clear();
    }
    public void AddButton(Sprite Icon, Action onClick)
    {
        int index = actioncalls.Count;
        Buttons[index].gameObject.SetActive(true);
        Buttons[index].GetComponent<Image>().sprite = Icon;
        actioncalls.Add(onClick);
    }
    public void OnButtonCLick(int index)
    {
        actioncalls[index]();
    }
	// Use this for initialization
	void Start ()
    {
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
