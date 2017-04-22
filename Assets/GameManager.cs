using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	static public int years = 0;

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
