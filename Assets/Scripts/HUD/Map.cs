using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {

	public RectTransform ViewPort;
	public Transform Corner1, Corner2;
	public GameObject BlipPrefab;
	public static Map Current;
    public bool mouseIsOverMap;
    public GameObject topMapCorner;
    public GameObject botMapCorner;

    private float heightRatio;
    private float widthRatio;


    private Vector2 terrainSize; 

	private RectTransform mapRect;

	public Map()
	{
		Current = this;
	}

	// Use this for initialization
	void Start () {

        terrainSize = new Vector2 (
			Corner2.position.x - Corner1.position.x,
			Corner2.position.z - Corner1.position.z);

		mapRect = GetComponent<RectTransform> ();
	}
    public void IsOnMiniMap()
    {
        if(
            Input.mousePosition.x > botMapCorner.transform.position.x &&
            Input.mousePosition.y > botMapCorner.transform.position.y &&
            Input.mousePosition.x < topMapCorner.transform.position.x &&
            Input.mousePosition.y < topMapCorner.transform.position.y
          )
        {
            
            mouseIsOverMap = true;
            return;
        }
        mouseIsOverMap = false;
        
    }

    public Vector3 MapPositionToWorld(Vector2 point)
    {   
        var worldPos = new Vector3
            (
             (point.x / topMapCorner.transform.position.x) * Corner2.transform.position.x,
             0,
             (point.y / topMapCorner.transform.position.y) * Corner2.transform.position.z
            );
        worldPos.y = Terrain.activeTerrain.SampleHeight(worldPos);

        return worldPos;
    }
	public Vector2 WorldPositionToMap(Vector3 point)
	{
		var pos = point - Corner1.position;
        var mapPos = new Vector2(
            point.x / terrainSize.x *  (topMapCorner.transform.position.x),
            point.z / terrainSize.y * (topMapCorner.transform.position.y));
		return mapPos;
	}
	
	// Update is called once per frame
	void Update () {
        IsOnMiniMap();
        widthRatio = (mapRect.rect.width / (float)Camera.main.pixelWidth);
        heightRatio = (mapRect.rect.height / (float)Camera.main.pixelHeight);
        ViewPort.position = WorldPositionToMap(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 40));
	}
}
