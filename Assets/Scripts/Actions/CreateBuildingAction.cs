using UnityEngine;
using System.Collections;

public class CreateBuildingAction : ActionBehaviour
{
    public GameObject GhostbuildingPrefab;
    private GameObject active = null;
	public override System.Action GetClickAction ()
	{
		return delegate() {
			var go = Instantiate(GhostbuildingPrefab);
            go.AddComponent<FindBuildingSite>();
            active = go;

		};
	}
    private void Update()
    {
        if (active == null) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            GameObject.Destroy(active);
        }
    }
    private void OnDestroy()
    {
        if (active == null) return;

        Destroy(active);
    }
}
