using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehavior : MonoBehaviour {
	
	[HideInInspector] public Rigidbody2D body;
	[HideInInspector] public float mass;
	public bool hasGravity;
	public bool isAffectedByGravity;
	public bool affectsPlayer;

	// Use this for initialization
	public virtual void Start () {
		body = this.GetComponent<Rigidbody2D>();
		mass = body.mass;
		GravityManager.RegisterGravityObject(this);

		// Uncomment below to add random starting force
		//body.AddForce(new Vector2(Random.Range(-200f,200f), Random.Range(-200f,200f)));
	}

	public virtual void UpdateMass(float newMass){
		body.mass = mass = newMass;
	}

	public virtual void UnregisterGravityObject(){
		GravityManager.UnregisterGravityObject(this);
	}

	public Vector2 GetPosition()
	{
		return transform.position;
	}

	public virtual void OnDestroy(){
		GravityManager.UnregisterGravityObject(this);
	}
}
