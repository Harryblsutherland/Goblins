using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public GameObject Canvas;
    private Image HealthBar;
    private Interactive thisUnit;

    void Start()
    {
        HealthBar = Canvas.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        thisUnit = GetComponent<Interactive>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Canvas.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) || HealthBar.fillAmount < 1 || thisUnit.Selected)
        {
            Canvas.SetActive(true);
        }
    }
}
