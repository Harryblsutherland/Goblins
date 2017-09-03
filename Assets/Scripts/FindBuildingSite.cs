using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildingSite : MonoBehaviour {

    public float maxBuildDistance = 30;
    public GameObject buildingPrefab;
    public PlayerSetupDefinition playerInfo;
    public Transform Source;

    Renderer Render;
    Color Red = new Color(1, 0, 0, 0.5f);
    Color green = new Color(0, 1, 0, 0.5f);
    // Update is called once per frame
    private void Start()
    {
        MouseManager.Current.enabled = false;
        Render = GetComponent<Renderer>();
    }
    void Update ()
    {
        var tempTarget = RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
        if (tempTarget.HasValue == false)
            return;

        transform.position = tempTarget.Value;

        if (Vector3.Distance(transform.position, Source.position) > maxBuildDistance)
        {
            Render.material.color = Red;
            return;
        }

        if (RtsManager.Current.IsGameObjectSafeToPlace(gameObject))
        {
            Render.material.color = green;
            if (Input.GetMouseButtonDown(0))
            {
                var go = Instantiate(buildingPrefab);
                go.AddComponent<ActionSelect>();
                go.transform.position = transform.position;
                go.AddComponent<Player> ().Info = playerInfo;
                go.GetComponent<StructureController>().RalliedObject = go;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Render.material.color = Red;
        }
        
	}
    private void OnDestroy()
    {
        MouseManager.Current.enabled = true;
    }

}

