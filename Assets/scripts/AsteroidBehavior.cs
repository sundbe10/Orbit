using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

	public float minSize;
	public float MaxSize;

	public GameObject explosion;

	void Start(){
		transform.localScale = Random.Range(minSize, MaxSize) * Vector3.one;
		StartCoroutine(CleanupAsteroid());
	}

	void OnCollisionEnter2D(Collision2D collision){
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	IEnumerator CleanupAsteroid(){
		yield return new WaitForSeconds(20);
		Destroy(gameObject);
	}
}
