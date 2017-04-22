using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralGridManager : Singleton<ProceduralGridManager> {

	class Grid {
		public int xLocator {get; set;}
		public int yLocator {get; set;}
		public GameObject gridObject {get; set;}
	}


	public float gridHeight;
	public float gridWidth;
	public GameObject playerObject;
	public GameObject gridObject;

	private List<Grid> gridManifest = new List<Grid>();

	void Awake() {
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Grid firstGrid = CreateGrid(playerObject.transform.position,0,0);
		CreateBorderGrids(firstGrid);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public void UpdateGrid(int xLocator, int yLocator){
		Instance.CreateBorderGrids(Instance.FindGrid(xLocator, yLocator));
	}

	Grid CreateGrid(Vector3 newPosition, int xLocator, int yLocator){
		var newGridObject = Instantiate(gridObject, newPosition, Quaternion.identity) as GameObject;
		newGridObject.GetComponent<ProceduralGridBehavior>().Initialize(gridWidth, gridHeight, xLocator, yLocator);

		Grid newGrid = new Grid{
			xLocator = xLocator,
			yLocator = yLocator,
			gridObject = newGridObject
		};

		gridManifest.Add(newGrid);
		return newGrid;
	}

	void CreateBorderGrids(Grid targetGrid){
		for(int i = -1; i <= 1; i++){
			for(int j = -1; j <= 1; j++){
				Debug.Log(i);
				if(i != 0 || j!= 0){
					if(!GridExistsAtLocator(targetGrid.xLocator + i, targetGrid.yLocator + j)){
						CreateGrid(targetGrid.gridObject.transform.position + new Vector3(i*gridWidth,j*gridHeight,0), targetGrid.xLocator + i, targetGrid.yLocator + j);
					}
				}
			}
		}
	}

	bool GridExistsAtLocator(int xLocator, int yLocator){
		var gridQuery = from grid in gridManifest 
			where grid.xLocator == xLocator
			&& grid.yLocator == yLocator
			select grid;
		return gridQuery.ToArray().Length != 0;
	}

	Grid FindGrid(int xLocator, int yLocator){
		var gridQuery = from grid in gridManifest 
				where grid.xLocator == xLocator
			&& grid.yLocator == yLocator
			select grid;
		return gridQuery.FirstOrDefault();
	}
}
