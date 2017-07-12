using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildingSite : MonoBehaviour {

    Renderer Render;
    Color Red = new Color(1, 0, 0, 0.5f);
    Color green = new Color(0, 1, 0, 0.5f);
    // Update is called once per frame
    private void Start()
    {
        Render = GetComponent<Renderer>();
    }
    void Update () {
        var tempTarget = RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
        if (tempTarget.HasValue == false)
            return;
        transform.position = tempTarget.Value;

        if (RtsManager.Current.IsGameObjectSafeToPlace(gameObject))
        {
            Render.material.color = green;
        }
        else
        {
            Render.material.color = Red;
        }
        
	}

}

