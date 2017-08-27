using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public string PlayerName;
    public float Confusion = 0.1f;
    public float Frequency = 1;
    
    private PlayerSetupDefinition player;
    private float waited = 0;
    private List<AIBehaviour> aiBehaviours = new List<AIBehaviour>();

    public PlayerSetupDefinition Player { get { return player; } } 


	// Use this for initialization
	void Start () {
        foreach (var AI in GetComponents<AIBehaviour>())
        {
            aiBehaviours.Add(AI);
        }
        foreach (var p in RtsManager.Current.Players)
        {
            if (p.Name == PlayerName)
            {
                player = p;
            }
        }
        gameObject.AddComponent<aiSupport>().Player = player;
	}
	
	// Update is called once per frame
	void Update () {
        waited += Time.deltaTime;
        if(waited < Frequency)
        {
            return;
        }
        string ailog = "";
        float BestAIValue = float.MinValue;
        AIBehaviour bestAI = null;
        aiSupport.GetSupport(gameObject).refresh();

        foreach(var ai in aiBehaviours)
        {
            ai.TimePassed += waited;
            var aiValue = (ai.GetWeight() * ai.WeightMultiplier) + Random.Range(0, Confusion);
            ailog += ai.GetType().Name + ":" + aiValue + "\n";
            if (aiValue > BestAIValue)
            {
                BestAIValue = aiValue;
                bestAI = ai;
            }
        }
        //Debug.Log(ailog);
        bestAI.Execute();
        waited = 0;


	}
}
