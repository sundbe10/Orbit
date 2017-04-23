using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStarManager : MonoBehaviour {

	private static BackgroundStarManager instance = null;
	public static Camera mainCamera;
	List<GameObject> stars;
	public GameObject star;
	GameObject backgroundStars;

	public float minDistance = 100f;
	public float maxDistance = 500f;
	static float bufferWidth = 12f;

	public int starCount = 100;

	public static BackgroundStarManager SharedInstance {
		get {
			if (instance == null) {
				instance = new BackgroundStarManager();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Awake () {
		mainCamera = Camera.main;
		stars = new List<GameObject>();
		backgroundStars = new GameObject();
		backgroundStars.name = "BackgroundStars";
		GenerateStars();
	}

	void GenerateStars() {
		while (stars.Count < starCount)
		{
			GameObject s = Instantiate<GameObject>(star);
			float z = Random.Range(minDistance, maxDistance);
			float frustrumHeight = FrustrumHeightAt(z);
			float frustrumWidth = frustrumHeight * mainCamera.aspect;
			float x = Random.Range(-0.5f*frustrumWidth, 0.5f*frustrumWidth);
			float y = Random.Range(-0.5f*frustrumHeight, 0.5f*frustrumHeight);
			s.transform.position = new Vector3(x,y,z);
			float size = ScaledValue(z, minDistance, maxDistance);
			s.transform.localScale *= size*5;
			s.transform.parent = backgroundStars.transform;
			stars.Add(s);
		}
	}

	static float ScaledValue(float input, float min, float max)
	{
		return (input-min)/(max-min);
	}

	public static float FrustrumHeightAt(float z)
	{
		return 2.0f * z * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
	}

	public static void MoveStar(BackgroundStarBehavior star)
	{
		if (mainCamera == null)
			return;
		Vector3 starPos = star.transform.position;
		float frustrumHeight = FrustrumHeightAt(starPos.z);
		float frustrumWidth = frustrumHeight * mainCamera.aspect;
		float maxY = mainCamera.transform.position.y + 0.5f*frustrumHeight;
		float minY = mainCamera.transform.position.y - 0.5f*frustrumHeight;
		float maxX = mainCamera.transform.position.x + 0.5f*frustrumWidth;
		float minX =mainCamera.transform.position.x - 0.5f*frustrumWidth;


		if (starPos.x < minX)
		{
			starPos += Vector3.right * (minX-starPos.x + frustrumWidth + bufferWidth);
		}
		else if (starPos.x > maxX)
		{
			starPos -= Vector3.right * (starPos.x-maxX + frustrumWidth + bufferWidth);
		}
		if (starPos.y < minY)
		{
			starPos += Vector3.up * (minY-starPos.y + frustrumHeight + bufferWidth);
		}
		else if (starPos.y > maxY)
		{
			starPos -= Vector3.up * (starPos.y-maxY + frustrumHeight + bufferWidth);
		}
		star.transform.position = starPos;
	}
}
