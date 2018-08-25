using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTriggerHandler : MonoBehaviour {


	public Dictionary<Triggers, List<ITriggerable>> Triggerlists = new Dictionary<Triggers, List<ITriggerable>>();
	// Use this for initialization
	void Start () {
		foreach (Triggers trigger in System.Enum.GetValues(typeof(Triggers)))
		{
			Triggerlists.Add(trigger, new List<ITriggerable>());
		}
	}
	public void FireTriggerList(Triggers trigger,GameObject triggeringUnit, GameObject targetedUnit)
	{
		foreach (var item in GetTriggeredList(trigger))
		{
			item.Trigger(triggeringUnit, targetedUnit);
		}
	}
	public List<ITriggerable> GetTriggeredList(Triggers trigger)
	{
		return Triggerlists[trigger];
	}

}
