using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	public GameObject asteroidObject;
	public float spawnTimeMin;
	public float spawnTimeMax;
	public float speedMin;
	public float speedMax;
	public float numAsteroids;

	GameObject playerObject;

	// Use this for initialization
	void Start () {
		GameManager.OnGameStateChange += GameStateChange;
		playerObject = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {

	}

	void GameStateChange(GameManager.State state){
		if(state == GameManager.State.ACTIVE){
			StartCoroutine(SpawnAsteroids(Random.Range(spawnTimeMin,spawnTimeMax)));
		}
	}

	IEnumerator SpawnAsteroids(float time){
		yield return new WaitForSeconds(time);
		StartCoroutine(SpawnAsteroids(Random.Range(spawnTimeMin,spawnTimeMax)));
		Vector3 playerVector = playerObject.transform.position - transform.position;
		Vector3 normalVector = new Vector3(playerVector.y, -playerVector.x, 0).normalized;
		if(playerVector.magnitude > 30){
			int asteroidsToSpawn = (int)Mathf.Floor(Random.Range(1, numAsteroids));
			for(int i = 0; i < asteroidsToSpawn; i++){
				Vector3 position = normalVector * Random.Range(-10, 10);
				GameObject asteroid = Instantiate(asteroidObject, transform.position + position, Quaternion.identity) as GameObject;
				asteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(playerVector.x, playerVector.y).normalized * Random.Range(speedMin, speedMax);
			}
		}
	}

	void OnDestroy(){
		GameManager.OnGameStateChange -= GameStateChange;
	}
}
