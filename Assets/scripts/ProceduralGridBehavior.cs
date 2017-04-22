using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGridBehavior : MonoBehaviour {

	[System.Serializable]
	public class ProceduralObject{
		public GameObject spawnObject;
		[Range(0,1)]
		public float frequency;
	}
		
	public ProceduralObject[] proceduralObjects;
	public float gridHeight;
	public float gridWidth;

	private int xLocator;
	private int yLocator;

	// Use this for initialization
	void Start () {
		CreateObjects();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth, gridHeight, 0));
	}

	void OnTriggerEnter2D(Collider2D collider){
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

	void CreateObjects(){
		foreach(ProceduralObject pObject in proceduralObjects){
			if(Random.value < pObject.frequency){
				GameObject newObject = Instantiate(pObject.spawnObject, transform.position+ new Vector3(Random.Range(-gridWidth/3, gridWidth/3), Random.Range(-gridHeight/3, gridHeight/3),  0), Quaternion.identity) as GameObject;
				newObject.transform.parent = transform;
			}
		}
	}
}
