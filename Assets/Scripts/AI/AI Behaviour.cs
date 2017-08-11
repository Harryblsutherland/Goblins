using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour {

    public float WeightMultiplier;

    public float TimePassed;

    public abstract float GetWeight();

    public abstract void Execute();
}

