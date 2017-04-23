using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : GravityBehavior {

	public float minSize;
	public float maxSize;

	public override void Start(){
		base.Start();
		float size = Random.Range(minSize, maxSize);
		Debug.Log(size);
		transform.localScale = Vector3.one * size;
		UpdateMass(mass * size + 1);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Star"){
			Destroy(gameObject);
			GravityManager.RemoveGravityObject(this);
		}
	}
		
}
