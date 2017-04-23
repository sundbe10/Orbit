using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrackerBehavior : MonoBehaviour {

	public delegate void OnStarChangeDelegate (float distance);
	public static event OnStarChangeDelegate OnStarLocated;
	public static event OnStarChangeDelegate OnStarLeave;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D collider){
		if(collider.tag == "Star"){
			OnStarLocated((transform.position - collider.transform.position).magnitude);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Star"){
			OnStarLeave(0);
		}
	}
}
