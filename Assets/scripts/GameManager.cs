using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	static public int years = 0;
	static public float health = 50;
	static public float maxHealth = 100;

	int yearCounter;

	void Awake() {
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
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
	}
}
