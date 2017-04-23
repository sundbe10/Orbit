using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehavior : MonoBehaviour {
	
	[HideInInspector] public Rigidbody2D body;
	[HideInInspector] public float mass;
	[HideInInspector] public float radius;
	public bool hasGravity;
	public bool isAffectedByGravity;

	// Use this for initialization
	public virtual void Start () {
		body = this.GetComponent<Rigidbody2D>();
		mass = body.mass;
		GravityManager.RegisterGravityObject(this);
		radius = transform.localScale.x;

		// Uncomment below to add random starting force
		//body.AddForce(new Vector2(Random.Range(-200f,200f), Random.Range(-200f,200f)));
	}

	public virtual void UpdateMass(float newMass){
		body.mass = mass = newMass;
	}

	public Vector2 GetPosition()
	{
		return transform.position;
	}
}
