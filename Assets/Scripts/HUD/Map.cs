using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {
    
	public RectTransform ViewPort;
	public Transform Corner1, Corner2;
	public GameObject BlipPrefab;
	public static Map Current;
    public GameObject mapcorner;

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
    void CalculateMapSize()
    {


    

    }
	public Vector2 WorldPositionToMap(Vector3 point)
	{
		var pos = point - Corner1.position;
        var mapPos = new Vector2(
            point.x / terrainSize.x *  (mapcorner.transform.position.x),
            point.z / terrainSize.y * (mapcorner.transform.position.y));
		return mapPos;
	}
	
	// Update is called once per frame
	void Update () {
        widthRatio = (mapRect.rect.width / (float)Camera.main.pixelWidth);
        heightRatio = (mapRect.rect.height / (float)Camera.main.pixelHeight);
        ViewPort.position = WorldPositionToMap(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 40));
	}
}
