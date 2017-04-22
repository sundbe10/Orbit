using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized*6);
	}
}
