using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBlip : MonoBehaviour {


    private GameObject _Blip;

    public GameObject Blip
    {
        get
        {
            return _Blip;
        }

        set
        {
            _Blip = value;
        }
    }

    // Use this for initialization
    void Start () {
        _Blip = Instantiate(Map.Current.BlipPrefab);
        _Blip.transform.parent = Map.Current.transform;
        _Blip.GetComponent<Image>().color = GetComponent<Player>().Info.AccentColor;
	}
	void OnDestroy()
    {
        Destroy(_Blip);  
            }
	// Update is called once per frame
	void Update () {
        _Blip.transform.position = Map.Current.WorldPositionToMap(transform.position);

    }
}
