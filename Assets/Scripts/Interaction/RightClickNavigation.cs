using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


 public class RightClickNavigation : Interaction {

    public float RelaxDistance = 5;
    private CommandManager commandManager;
    private NavMeshAgent agent;
    private Vector3 target = Vector3.zero;
    private bool selected = false;
    private bool isActive = false;

    public override void Deselect()
    {
        selected = false;
    }
    public override void Select()
    {
        selected = true;
    }
    public void SendToTarget(Vector3 pos)
    {
        target = pos;
        SendToTarget();
    }
    public void SendToTarget()
    {
        agent.SetDestination(target);
        agent.isStopped = false;
        isActive = true;
    }
    // Use this for initialization
    void Start () {

        commandManager = GetComponent<CommandManager>();
        agent = GetComponent<NavMeshAgent>();

    }
    // Update is called once per frame
	void Update () {
        if (selected && Input.GetKey(KeyCode.P))
        {
            Vector3 tempTarget = (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
            commandManager.AddCommand(Cmd_Patrol.New(transform.gameObject, tempTarget));
        }
        if (selected && Input.GetKey(KeyCode.F))
        {
            Vector3 tempTarget = (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
            commandManager.AddCommand(Cmd_Patrol.New(transform.gameObject, tempTarget));
        }
    }
}
