using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoManager : MonoBehaviour {

	public static InfoManager Current;

	public Image PortraitImage;
	public Text Line1, Line2, Line3;

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
    }

	public void SetImage(Sprite Image)
	{
        PortraitImage.sprite = Image;
        PortraitImage.color = Color.white;
	}

	// Use this for initialization
	void Start () {
        Clearbox();
	}
}
