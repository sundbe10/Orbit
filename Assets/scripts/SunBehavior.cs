using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior: MonoBehaviour {

	[System.Serializable]
	public class StarPhase{
		[Range(0,1)]
		public float lifePercentMax;
		public Color emissionColor;
		public Color materialColor;
		[Range(0,8)]
		public float size;
	}

	public StarPhase[] starPhases;
	public float totalLifetime = 100;
	public float currentLifetime = 0;
	public GameObject explosionObject;

	float lifeCounter;
	Transform bodyTransform;
	MeshRenderer topMesh;
	MeshRenderer surfaceMesh;
	bool canAge = false;
	SunSoundBehavior sunSound;

	// Use this for initialization
	void Start () {
		bodyTransform = transform.Find("Body");
		topMesh = transform.Find("Body/Top").GetComponent<MeshRenderer>();
		surfaceMesh = transform.Find("Body/Surface").GetComponent<MeshRenderer>();
		CloneMesh(topMesh);
		CloneMesh(surfaceMesh);
		sunSound = GetComponent<SunSoundBehavior>();

		currentLifetime = Random.Range(0, totalLifetime * 0.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdatePhase();
		if(currentLifetime >= totalLifetime) Explode();
		sunSound.size = bodyTransform.localScale.x;
	}

	public void StartAging(){
		Debug.Log("start againg");
		canAge = true;
	}

	void UpdatePhase(){
		if(canAge){
			if(currentLifetime < totalLifetime) currentLifetime ++ ;
		}
		StarPhase currentPhase = null;
		foreach(StarPhase starPhase in starPhases){
			if(currentLifetime <= totalLifetime* starPhase.lifePercentMax && currentPhase == null){
				currentPhase = starPhase;
			}
		}
		if(currentPhase != null){
			topMesh.material.color = Color32.Lerp(topMesh.material.color, currentPhase.materialColor, Time.deltaTime);
			surfaceMesh.material.color = Color32.Lerp(surfaceMesh.material.color, currentPhase.materialColor, Time.deltaTime);

			topMesh.material.SetColor("_EmissionColor", Color32.Lerp(topMesh.material.GetColor("_EmissionColor"), currentPhase.emissionColor, Time.deltaTime));
			surfaceMesh.material.SetColor("_EmissionColor", Color32.Lerp(surfaceMesh.material.GetColor("_EmissionColor"), currentPhase.emissionColor, Time.deltaTime));

			bodyTransform.localScale = Vector3.one * Mathf.Lerp(bodyTransform.localScale.x, currentPhase.size, Time.deltaTime);
		}

		if (currentPhase == starPhases[3] && !sunSound.isCollapsing)
			sunSound.Collapse();
	}

	void CloneMesh(MeshRenderer childMesh){
		Material newMat = new Material(Shader.Find("Standard"));
		newMat.CopyPropertiesFromMaterial(childMesh.material); 
		childMesh.material = newMat;
	}

	void Explode(){
		GameObject explosion =  Instantiate(explosionObject, transform.position, Quaternion.identity);
		explosion.transform.parent = transform.parent;
		bodyTransform.gameObject.GetComponent<GravityBehavior>().UnregisterGravityObject();
		Destroy(bodyTransform.gameObject);

		//Propel children
		Transform[] children = transform.GetComponentsInChildren<Transform>();
		foreach(Transform child in children){
			if(child.tag == "Planet"){
				child.parent = transform.parent;
				child.GetComponent<Rigidbody2D>().AddForce((child.transform.position - transform.position).normalized * Mathf.Sqrt((child.transform.position - transform.position).magnitude) * 50f);
			}
		}

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * Mathf.Sqrt((player.transform.position - transform.position).magnitude));
		Destroy(gameObject);
	}

	
}
