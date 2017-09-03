using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour {
    
    public abstract void RightClickInSpace(Vector3 Point);
    public abstract void RightClickOnUnit(GameObject TargetUnit);
    public abstract void RightClickOnStructure(GameObject TargetUnit);




}
