using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitAction : ActionBehaviour {

    public GameObject CreatedUnit;
    public float Cost = 0;
    private PlayerSetupDefinition Player;

	// Use this for initialization
	void Start ()
    {
        Player = GetComponent<Player>().Info;

    }
	public override System.Action GetClickAction()
    {
        return delegate ()
        {
            if (Player.Credits < Cost)
            {
                Debug.Log("Cannot Create, It costs" + Cost);
                return;
            }
            var go = (GameObject)GameObject.Instantiate(
                                                        CreatedUnit,
                                                        transform.position,
                                                        Quaternion.identity);
            go.AddComponent<Player>().Info = Player;
            go.AddComponent<RightClickNavigation>();
            go.AddComponent<ActionSelect>();
            Player.Credits -= Cost;

        };
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
}
