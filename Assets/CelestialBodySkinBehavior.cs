using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBodySkinBehavior : MonoBehaviour {

	public Material[] startingMaterials;

	// Use this for initialization
	void Start () {
		Material newMat = new Material(Shader.Find("Standard"));
		newMat.CopyPropertiesFromMaterial(startingMaterials[0]); 
		newMat.color = Random.ColorHSV(0f,1f,0.8f,0.8f,1f,1f);
		GetComponentInChildren<MeshRenderer>().materials = new Material[]{newMat};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
