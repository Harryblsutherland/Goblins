using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : InputController
{

    private Vector3 rallypoint;
    private GameObject ralliedObject;
    public GameObject RalliedObject
    {
        get
        {
            return ralliedObject;
        }

        set
        {
            ralliedObject = value;
        }
    }

    public Vector3 Rallypoint
    {
        get
        {
            return rallypoint;
        }

        set
        {
            rallypoint = value;
        }
    }

    public override void RightClickInSpace(Vector3 Point)
    {
        Rallypoint = Point;
        ralliedObject = null;
        transform.GetChild(0).transform.position = Point;

    }

    public override void RightClickOnMine(GameObject TargetObject)
    {
        rallypoint = transform.position;
        RalliedObject = TargetObject;
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 2);
    }

    public override void RightClickOnStructure(GameObject prTargetUnit)
    {
        rallypoint = transform.position;
        RalliedObject = prTargetUnit;
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 2);

    }

    public override void RightClickOnUnit(GameObject prTargetUnit)
    {
        rallypoint = transform.position;
        RalliedObject = prTargetUnit;
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 2);
    }

    // Use this for initialization
    void Start()
    {
        RalliedObject = transform.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (RalliedObject == null)
        {
            return;
        }
        if (RalliedObject.gameObject != transform.gameObject)
        {
            transform.GetChild(0).transform.position = RalliedObject.transform.position;
        }
    }
}
