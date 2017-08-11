using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class ActionBehaviour : MonoBehaviour {
    public abstract Action GetClickAction();
    public Sprite ButtonIcon;

}
