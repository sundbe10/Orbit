using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBodySkinBehavior : MonoBehaviour {

	[System.Serializable]
	public class PlanetComponent{
		public GameObject component;
		[Range(0,1)]
		public float saturationMin;
		[Range(0,1)]
		public float saturationMax;
	}

	public PlanetComponent[] planetComponents;

	// Use this for initialization
	void Awake () {
		foreach(PlanetComponent planetComponent in planetComponents){
			ChangeColor(planetComponent.component.GetComponent<MeshRenderer>(), planetComponent.saturationMin, planetComponent.saturationMax);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeColor(MeshRenderer childMesh, float satMin, float satMax){
		Material newMat = new Material(Shader.Find("Standard"));
		newMat.CopyPropertiesFromMaterial(childMesh.material); 
		newMat.color = Random.ColorHSV(0f,1f,satMin,satMax,1f,1f);
		childMesh.materials = new Material[]{newMat};
	}
}
