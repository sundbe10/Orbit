using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	static public int years = 0;
	static public float health = 100;
	static public float maxHealth = 100;

	int yearCounter;
	float healthDecreaseSpeed = -0.05f;
	float healthIncreaseSpeed = 0;

	void Awake() {
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		StarTrackerBehavior.OnStarLeave += StarLeave;
		StarTrackerBehavior.OnStarLocated += StarLocated;
		healthIncreaseSpeed = healthDecreaseSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		yearCounter++;
		if(yearCounter == 10){
			years++;
			yearCounter = 0;
		}

		// change health
		health += healthIncreaseSpeed + healthDecreaseSpeed;
		if(health > maxHealth) health = maxHealth;
		if(health < 0) health = 0;
	}

	void StarLocated(float distance){
		Debug.Log(distance);
		healthIncreaseSpeed = 1/distance;
	}

	void StarLeave(float distance){
		healthIncreaseSpeed = 0;
	}
}
