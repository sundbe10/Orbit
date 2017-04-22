using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGridBehavior : MonoBehaviour {

	[System.Serializable]
	public class ProceduralObject{
		public GameObject spawnObject;
		public int frequency;
	}
		
	public ProceduralObject[] proeduralObjects;
	public float gridHeight;
	public float gridWidth;

	private int xLocator;
	private int yLocator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth, gridHeight, 0));
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log("Collide!");
		if(collider.gameObject.tag == "Player"){
			ProceduralGridManager.UpdateGrid(xLocator, yLocator);
		}
	}

	public void Initialize(float width, float height, int x, int y){
		gridWidth = width;
		gridHeight = height;
		xLocator = x;
		yLocator = y;

		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		collider.size = new Vector2(width, height);
	}
}
