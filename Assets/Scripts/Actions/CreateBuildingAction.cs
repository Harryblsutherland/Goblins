using UnityEngine;
using System.Collections;

public class CreateBuildingAction : ActionBehaviour
{
    public float buildingCost;
    public GameObject buildingPrefab;
    public float maxBuildDistance;
    public GameObject GhostbuildingPrefab;
    private GameObject active = null;

    public override System.Action GetClickAction ()
	{
		return delegate() {

            var player = GetComponent<Player>().Info;
            if (player.Credits < buildingCost)
            {
                Debug.Log("You Are Broke!");
                return;
            }
            player.Credits -= buildingCost;

            var go = Instantiate(GhostbuildingPrefab);
            var siteFinder = go.AddComponent<FindBuildingSite>();
            siteFinder.buildingPrefab = buildingPrefab;
            siteFinder.maxBuildDistance = maxBuildDistance;
            siteFinder.playerInfo = player;
            siteFinder.Source = transform;
            active = go;

		};
	}
    private void Update()
    {
        if (active == null) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Destroy(active);
        }
    }
    private void OnDestroy()
    {
        if (active == null) return;
        Destroy(active);
    }
}
