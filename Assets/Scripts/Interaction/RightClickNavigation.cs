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
            if ((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && commandManager.commandQueue.Count > 0))
            {
                commandManager.FlushList();
            }


            commandManager.commandQueue.Add(new Cmd_Patrol(tempTarget, commandManager, agent));
        }



        if (selected && Input.GetMouseButtonDown(1))
        {
            var commandmanager = GetComponent<CommandManager>();
            
            Vector3 tempTarget = (Vector3)RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
            if ((!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && commandmanager.commandQueue.Count > 0))
            {
                commandmanager.FlushList();
            }

                      
            commandmanager.commandQueue.Add(new Cmd_Move(tempTarget,commandmanager,agent));

        }
	}
}
