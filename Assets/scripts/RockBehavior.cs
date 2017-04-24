using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour {

	[System.Serializable]
	public class PlanetPhase{
		public Texture groundTexture;
		public Texture cloudTexture;
		public float lifePercentMax;
		[HideInInspector]
		public Material cloudMaterial;
		[HideInInspector]
		public Material groundMaterial;
	}

	public bool emitShine = true;

	public PlanetPhase[] planetPhases;
	public float maxSunTime;
	public float currentSunTime = 0;

	private MeshRenderer cloudMesh;
	private MeshRenderer groundMesh;
	private ParticleSystem emitter;
	private SunBehavior sunBehavior;
	private CircleCollider2D collider;
	private AudioSource shineSound;

	bool sunActive = false;

	// Use this for initialization
	void Start () {

		GameManager.OnGameStateChange += StartSunTracking;
		if(GameManager.GetState() == GameManager.State.ACTIVE) sunActive = true;

		currentSunTime = maxSunTime/2;

		cloudMesh = transform.Find("Clouds").GetComponent<MeshRenderer>();
		groundMesh = transform.Find("Ground").GetComponent<MeshRenderer>();
		emitter = GetComponentInChildren<ParticleSystem>();
		emitter.enableEmission = false;
		shineSound = GetComponent<AudioSource>();

		collider = GetComponent<CircleCollider2D>();
		collider.radius = collider.radius/transform.parent.localScale.x;

		foreach(PlanetPhase planetPhase in planetPhases){
			Material newGroundMaterial = new Material(Shader.Find("Standard"));
			Material newCloudMaterial = new Material(Shader.Find("Standard"));
			newGroundMaterial.CopyPropertiesFromMaterial(groundMesh.material);
			newCloudMaterial.CopyPropertiesFromMaterial(cloudMesh.material);
			newGroundMaterial.SetTexture("_MainTex", planetPhase.groundTexture);
			newCloudMaterial.SetTexture("_MainTex", planetPhase.cloudTexture);
			planetPhase.groundMaterial  = newGroundMaterial;
			planetPhase.cloudMaterial = newCloudMaterial;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(currentSunTime > 0 && sunActive) currentSunTime--;
		UpdatePhase();
	}

	void OnTriggerStay2D(Collider2D collider){
		if(collider.tag == "Star" && sunActive){
			if((collider.transform.position - transform.position).magnitude > collider.transform.localScale.x){
				if(currentSunTime < maxSunTime) currentSunTime += 20/(collider.transform.position - transform.position).magnitude;
				Shine(true);
			}else{
				Shine(false);
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Star"){
			Shine(false);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Star"){
			currentSunTime = 0;
		}else if(collision.collider.tag == "Planet" && sunActive){
			currentSunTime -= maxSunTime/6;
		}
	}

	void UpdatePhase(){
		PlanetPhase currentPhase = null;
		foreach(PlanetPhase planetPhase in planetPhases){
			if(currentSunTime <= maxSunTime * planetPhase.lifePercentMax && currentPhase == null){
				currentPhase = planetPhase;
			}
		}
		if(currentPhase != null){
			cloudMesh.material = currentPhase.cloudMaterial;
			groundMesh.material = currentPhase.groundMaterial;
		}
	}

	void Shine(bool enable){
		if(enable){
			emitter.enableEmission = true;
			if (!shineSound.isPlaying)
			{
				StopAllCoroutines();
				shineSound.time = Random.Range(0f, 3f);
				shineSound.pitch = Random.Range(.95f, 1.05f);
				StartCoroutine(FadeShineSound(0.05f, .2f));
			}
		}else{
			emitter.enableEmission = false;
			if (shineSound.isPlaying)
			{
				StopAllCoroutines();
				StartCoroutine(FadeShineSound(0f, .6f));
			}
		}
	}

	IEnumerator FadeShineSound(float targetVolume, float fadeTime)
	{
		if (!shineSound.isPlaying)
			shineSound.Play();

		float startVolume = shineSound.volume;
		float timer = 0f;

		while (shineSound.volume != targetVolume)
		{
			timer += Time.deltaTime/fadeTime;
			shineSound.volume = Mathf.Lerp(startVolume, targetVolume, timer);
			yield return null;
		}

		if (shineSound.volume <= 0)
			shineSound.Stop();
	}

	void StartSunTracking(GameManager.State state){
		if(state == GameManager.State.ACTIVE) sunActive = true;
	}

	void OnDestroy(){
		GameManager.OnGameStateChange -= StartSunTracking;
	}

}
