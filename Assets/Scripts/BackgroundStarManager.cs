using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStarManager : MonoBehaviour {

	Camera mainCamera;
	List<GameObject> stars;
	public GameObject star;

	public float minDistance = 100;
	public float maxDistance = 500;

	public float starCount = 100;

	// Use this for initialization
	void Awake () {
		mainCamera = Camera.main;
		stars = new List<GameObject>();
		GenerateStars();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateStars() {
		while (stars.Count < starCount)
		{
			GameObject s = Instantiate<GameObject>(star);
			float z = Random.Range(minDistance, maxDistance);
			float frustrumWidth = FrustrumWidthAt(z);
			float frustrumHeight = frustrumWidth * mainCamera.aspect;
			float x = Random.Range(-0.5f*frustrumWidth, 0.5f*frustrumWidth);
			float y = Random.Range(-0.5f*frustrumHeight, 0.5f*frustrumHeight);
			s.transform.position = new Vector3(x,y,z);

			stars.Add(s);
		}
	}

	float ScaledValue(float input, float min, float max)
	{
		return (input-minDistance)/(max-min);
	}

	float FrustrumWidthAt(float z)
	{
		return 2.0f * z * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
	}
}
