using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : GravityBehavior {

	public float minSize;
	public float maxSize;
	public GameObject offspring;
	public int population;
	public GameObject[] planetTemplates;

	public override void Start(){

		GameObject planet =  Instantiate(planetTemplates[(int)Mathf.Floor(Random.Range(0, planetTemplates.Length))], transform.position, Quaternion.identity) as GameObject;
		planet.transform.parent = transform;

		base.Start();
		float size = Random.Range(minSize, maxSize);
		Debug.Log(size);
		transform.localScale = Vector3.one * size;
		UpdateMass(mass * size + 1);
	}

	void OnAwake() {
		population = 0;
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

	public void SpawnOffspring(){
		GameObject go = Instantiate(offspring, transform, false);
		go.transform.RotateAround(transform.position, Vector3.forward, Random.Range(0f, 359f));
		++population;
	}
}
