﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public GameObject explosionObject;

	public delegate void OnHealthChangeDelegate (float health);
	public static event OnHealthChangeDelegate OnHealthChange;
	public static event OnHealthChangeDelegate OnSetHealth;

	RockBehavior planet;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		planet = GetComponentInChildren<RockBehavior>();
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.AddForce(Vector2.up*10);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*6);
		OnSetHealth(planet.maxSunTime);
		OnHealthChange(planet.currentSunTime);

		if(planet.currentSunTime <= 0){
			Explode();
		}
	}

	void Explode(){
		Instantiate(explosionObject, transform.position, Quaternion.identity);
		GetComponent<GravityBehavior>().UnregisterGravityObject();
		rigidBody.Sleep();
		this.enabled = false;
		Destroy(transform.Find("Rock Planet").gameObject);
	}
}
