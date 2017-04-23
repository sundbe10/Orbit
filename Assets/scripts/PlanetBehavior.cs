using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : GravityBehavior {

	[System.Serializable]
	public class PlanetTemplate{
		public GameObject template;
		public float minSize;
		public float maxSize;
	}
		
	public GameObject offspring;
	public int population;
	public PlanetTemplate[] planetTemplates;
	public float size;

	public float distance;

	public override void Start(){

		PlanetTemplate planet;
		Transform sun =  GetComponentInParent<SunBehavior>().transform;
		if((sun.position - transform.position).magnitude > 10 && (sun.position - transform.position).magnitude < 15){
			planet = planetTemplates[0];
		}else{
			planet = planetTemplates[1];
		}


		GameObject newPlanet =  Instantiate(planet.template, transform.position, Quaternion.identity) as GameObject;
		newPlanet.transform.parent = transform;

		base.Start();
		size = Random.Range(planet.minSize, planet.maxSize);
		transform.localScale = Vector3.one * size;
		UpdateMass(mass * size + 1);
	}

	void OnAwake() {
		population = 0;
	}

	// Update is called once per frame
	void Update () {
		SunBehavior sun = GetComponentInParent<SunBehavior>();
		if(sun != null){
			distance = (sun.transform.position - transform.position).magnitude;
		}
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
