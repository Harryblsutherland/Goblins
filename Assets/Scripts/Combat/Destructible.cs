using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    private UnitInfo info;
    public GameObject Corpse;
    // Use this for initialization
    void Start()
    {
        info = GetComponent<UnitInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        if (info.currentHealth <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Instantiate(Corpse, transform.position, Quaternion.identity);
        }
    }
}
