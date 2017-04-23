using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	static public int years = 0;
	static public float health = 100;
	static public float maxHealth = 100;

	int yearCounter;

	void Awake() {
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		PlayerBehavior.OnHealthChange += HealthChange;
		PlayerBehavior.OnSetHealth += SetHealth;
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

	void SetHealth(float health){
		maxHealth = health;
	}

	void HealthChange(float newHealth){
		health = newHealth;
	}
}
