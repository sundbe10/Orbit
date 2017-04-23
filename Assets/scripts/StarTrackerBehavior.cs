using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrackerBehavior : MonoBehaviour {

	public delegate void OnStarChangeDelegate ();
	public static event OnStarChangeDelegate onStarLocated;
	public static event OnStarChangeDelegate onStarLeave;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnStayStay2D(Collider2D collider){
		if(collider.tag == "Star"){
			onStarLocated();
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Star"){
			onStarLeave();
		}
	}
}
