using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : GravityBehavior {

	public float minSize;
	public float maxSize;
	public GameObject offspring;
	public int population;
	public GameObject[] planetTemplates;
	public float size;
	AudioHighPassFilter hiPass;
	AudioListener listener;
	Rigidbody2D listenerBody;
	AudioSource planetAudio;
	float cutoff;
	float volume;

	public override void Start(){
		listener = GameObject.FindObjectOfType<AudioListener>();
		listenerBody = listener.GetComponent<Rigidbody2D>();
		hiPass = GetComponent<AudioHighPassFilter>();
		planetAudio = GetComponent<AudioSource>();
		GameObject planet =  Instantiate(planetTemplates[(int)Mathf.Floor(Random.Range(0, planetTemplates.Length))], transform.position, Quaternion.identity) as GameObject;
		planet.transform.parent = transform;

		base.Start();
		size = Random.Range(minSize, maxSize);
		transform.localScale = Vector3.one * size;
		UpdateMass(mass * size + 1);
		planetAudio.pitch = 0.8f/size;
	}

	void OnAwake() {
		population = 0;
		cutoff = 0f;
		volume = 0f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float soundDistance = Vector2.Distance(listener.transform.position, transform.position);
		if (soundDistance < planetAudio.maxDistance)
		{
			cutoff = Mathf.SmoothStep(cutoff, Mathf.Clamp((soundDistance-1f)*50f, 20f, 400f), 10*Time.deltaTime);
			hiPass.cutoffFrequency = cutoff;
			volume = Mathf.SmoothStep(volume, (body.velocity.sqrMagnitude/100f)*0.3f + 0.7f*(listenerBody.velocity - body.velocity).sqrMagnitude/100f, 10*Time.deltaTime);
			planetAudio.volume = Mathf.Clamp(volume, 0f, 0.7f);
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
