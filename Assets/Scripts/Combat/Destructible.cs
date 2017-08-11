using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    private ShowUnitInfo info;
    public GameObject Corpse;
    // Use this for initialization
    void Start()
    {
        info = GetComponent<ShowUnitInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        if (info.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Instantiate(Corpse, transform.position, Quaternion.identity);
        }
    }
}
