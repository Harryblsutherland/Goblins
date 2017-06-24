using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour {
    public float timebetweenCHecks = 1;
    public float visibleRange = 100;
    private float waited = 10000;

	
	// Update is called once per frame
	void Update () {
        waited += Time.deltaTime;
        if (waited <= timebetweenCHecks) return;

        waited = 0;

        List<MapBlip> pblips = new List<MapBlip>();
        List<MapBlip> oblips = new List<MapBlip>();
        
        foreach (var p in RtsManager.Current.Players)
        {
            foreach (var u in p.ActiveUnits)
            {
                var blip = u.GetComponent<MapBlip>();
                if (p == Player.Default) pblips.Add(blip);
                else oblips.Add(blip);
            }
        }
        foreach (var o in oblips)
        {
            bool active = false;
            foreach (var p in pblips)
            {
                var distance = Vector3.Distance(o.transform.position, p.transform.position);
                if (distance >= visibleRange)
                {
                    active = true;
                    break;
                }

            }
            o.Blip.SetActive(active);
            foreach (var r in o.GetComponentsInChildren<Renderer>()) r.enabled = active;
           
        }
	}
}
