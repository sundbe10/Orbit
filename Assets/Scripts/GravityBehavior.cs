using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehavior : MonoBehaviour {
	
	public Rigidbody2D body;
	public float mass;


	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D>();
		mass = body.mass;
		GravityManager.RegisterGravityObject(this);

		// Uncomment below to add random starting force
		//body.AddForce(new Vector2(Random.Range(-200f,200f), Random.Range(-200f,200f)));
	}

	public Vector2 GetPosition()
	{
		return body.position;
	}
}
