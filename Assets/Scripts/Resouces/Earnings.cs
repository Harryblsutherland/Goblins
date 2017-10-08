using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earnings : MonoBehaviour {

    public float creditsPerSecond = 1;
    private PlayerSetupDefinition player;
	// Use this for initialization
	void Start ()
    {
        player = GetComponent<Player>().Info;
	}
	
	// Update is called once per frame
	void Update ()
    {
        player.Credits += creditsPerSecond * Time.deltaTime;
	}
}
