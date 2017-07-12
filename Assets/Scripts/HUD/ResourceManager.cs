using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {


    public Text PlayerGold;

	
	// Update is called once per frame
	void Update ()
    {
        PlayerGold.text = "$ " + (int)Player.Default.Credits;
	}
}
