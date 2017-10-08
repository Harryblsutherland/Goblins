using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankAction : ActionBehaviour {
    public override Action GetClickAction()
    {
        return () => { };
    }
}
