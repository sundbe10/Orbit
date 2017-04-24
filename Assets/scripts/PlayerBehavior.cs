using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	enum State{
		INACTIVE,
		ACTIVE
	}

	public GameObject explosionObject;

	public delegate void OnHealthChangeDelegate (float health);
	public static event OnHealthChangeDelegate OnHealthChange;
	public static event OnHealthChangeDelegate OnSetHealth;

	State state = State.INACTIVE;
	RockBehavior planet;
	Rigidbody2D rigidBody;

	void Awake(){
		planet = GetComponentInChildren<RockBehavior>();
		rigidBody = GetComponent<Rigidbody2D>();
		GameManager.OnGameStateChange += GameStateChange;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(state == State.ACTIVE){
			rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*6);
		}

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

	void GameStateChange(GameManager.State state){
		Debug.Log(state);
		switch(state){
		case GameManager.State.START:
			rigidBody.isKinematic = true;
			break;
		case GameManager.State.ACTIVE:
			rigidBody.isKinematic = false;
			rigidBody.AddForce(Vector2.one *20);
			ChangeState(State.ACTIVE);
			break;
		case GameManager.State.END:
			ChangeState(State.INACTIVE);
			rigidBody.isKinematic = true;
			break;
		}
	}

	void ChangeState(State newState){
		state = newState;
	}

	void OnDestroy(){
		GameManager.OnGameStateChange -= GameStateChange;
	}
}
