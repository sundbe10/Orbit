using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateSolarSystemBehavior : MonoBehaviour {

	public GameObject sunObject;
	public GameObject planetObject;
	public bool ageImmediately = false;

	private List<GameObject> planets = new List<GameObject>();
	private GameObject sun;

	// Use this for initialization
	void Start () {
		CreateSun();
		CreatePlanets((int)Mathf.Floor(Random.Range(3,7)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateSun(){
		sun = Instantiate(sunObject, transform.position, Quaternion.identity) as GameObject;
		sun.transform.parent = transform;
		if(ageImmediately) sun.GetComponent<SunBehavior>().StartAging();
	}

	void CreatePlanets(int numPlanets){
		for(int i = 0; i < numPlanets; i++){
			CreatePlanet();
		}
	}

	void CreatePlanet(){
		float randomAngle = Random.Range(0,360);
		float randomDistance = Random.Range(6,20);
		Quaternion Rot = Quaternion.AngleAxis(randomAngle, Vector3.forward);
		Vector3 planetVector = Rot * Vector3.right;
		Vector3 planetPosition = sun.transform.position + planetVector * randomDistance;

		GameObject newPlanet = Instantiate(planetObject, planetPosition, Quaternion.identity) as GameObject;
		newPlanet.transform.parent = sun.transform;

		// Add normal force to create orbit 
		Vector2 normalVector = new Vector2(planetVector.y, -planetVector.x);
		newPlanet.GetComponent<Rigidbody2D>().velocity = normalVector * GetPlanetInitialVelocity(randomDistance, sun.GetComponentInChildren<Rigidbody2D>().mass); 

		planets.Add(newPlanet);
	}

	float GetPlanetInitialVelocity(float orbitalRadius, float mass){
		return Mathf.Sqrt(GravityManager.G * mass /orbitalRadius);
	}
}
