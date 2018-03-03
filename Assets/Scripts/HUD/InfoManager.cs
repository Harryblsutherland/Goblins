using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class InfoManager : MonoBehaviour {

	public static InfoManager Current;

	public Image PortraitImage, progressBar;
	public List<Button> productionItems = new List<Button>();
	public Text Line1, Line2, Line3;
	public GameObject progressHud;
	public GameObject CurrentlyDisplayed;
	public Button Slot1, Slot2, Slot3, Slot4, Slot5;

	public InfoManager()
	{
		Current = this;
	}

	public void SetLines(string line1, string line2, string line3)
	{
		Line1.text = line1;
		Line2.text = line2;
		Line3.text = line3;
	}

	public void Clearbox()
	{
		SetLines ("", "", "");
		PortraitImage.color = Color.clear;
		progressHud.SetActive(false);
		progressBar.fillAmount = 0;
		foreach (var button in productionItems)
		{
			button.GetComponent<Image>().color = Color.clear;
		}
	}

	public void SetImage(Sprite Image)
	{
		PortraitImage.sprite = Image;
		PortraitImage.color = Color.white;
	}
	public void removeCurrentItem(int index)
	{
		CurrentlyDisplayed.GetComponent<ProductionManager>().removeItemFromList(index);
	}
	// Use this for initialization
	void Start () {
		Clearbox();

		productionItems.Add(Slot1);
		productionItems.Add(Slot2);
		productionItems.Add(Slot3);
		productionItems.Add(Slot4);
		productionItems.Add(Slot5);

		for (var i = 1; i < productionItems.Count; i++)
		{
			productionItems[i].onClick.AddListener(delegate { });
		}
	}
}
