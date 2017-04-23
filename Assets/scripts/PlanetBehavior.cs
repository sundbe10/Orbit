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
	public GameObject explosionObject;

	AudioHighPassFilter hiPass;
	AudioListener listener;
	Rigidbody2D listenerBody;
	AudioSource planetAudio;
	float cutoff;
	float volume;

	public float distance;

	public override void Start(){
		listener = GameObject.FindObjectOfType<AudioListener>();
		listenerBody = listener.GetComponent<Rigidbody2D>();
		hiPass = GetComponent<AudioHighPassFilter>();
		planetAudio = GetComponent<AudioSource>();

		PlanetTemplate planetTemplate;
		Transform sun =  GetComponentInParent<SunBehavior>().transform;
		if((sun.position - transform.position).magnitude > 11 && (sun.position - transform.position).magnitude < 15){
			planetTemplate = planetTemplates[0];
		}else{
			planetTemplate = planetTemplates[1];
		}
		GameObject newPlanet =  Instantiate(planetTemplate.template, transform.position, Quaternion.identity) as GameObject;
		newPlanet.transform.parent = transform;

		base.Start();
		size = Random.Range(planetTemplate.minSize, planetTemplate.maxSize);
		transform.localScale = Vector3.one * size;
		UpdateMass(mass * size + 1);
		planetAudio.pitch = ScaleClamp(size, planetTemplate.minSize, planetTemplate.maxSize, 1.4f, .7f);
		planetAudio.volume = 0f;
	}

	void OnAwake() {
		population = 0;
		cutoff = 0f;
		volume = 0f;
	}

	// Update is called once per frame
	void Update () {
		SunBehavior sun = GetComponentInParent<SunBehavior>();
		if(sun != null){
			distance = (sun.transform.position - transform.position).magnitude;
		}
	}

	void FixedUpdate () {
		float soundDistance = Vector2.Distance(listener.transform.position, transform.position);
		if (soundDistance < planetAudio.maxDistance)
		{
			cutoff = ScaleClamp(soundDistance, 2f, 8f, 20f, 400f);
			hiPass.cutoffFrequency = cutoff;
			volume = Mathf.SmoothStep(volume, (body.velocity.sqrMagnitude/50f)*0.3f + 0.7f*(listenerBody.velocity - body.velocity).sqrMagnitude/50f, 10*Time.deltaTime);
			planetAudio.volume = Mathf.Clamp(volume, 0f, 0.9f);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Star"){
			Instantiate(explosionObject, transform.position, Quaternion.identity);
			Destroy(gameObject);
			GravityManager.RemoveGravityObject(this);
		}
	}

	public void SpawnOffspring(){
		GameObject go = Instantiate(offspring, transform, false);
		go.transform.RotateAround(transform.position, Vector3.forward, Random.Range(0f, 359f));
		++population;
	}

	float ScaleClamp(float input, float inMin, float inMax, float outMin, float outMax)
	{
		return (((Mathf.Clamp(input, inMin, inMax) - inMin) * (outMax - outMin)) / (inMax - inMin)) + outMin;
	}
}
