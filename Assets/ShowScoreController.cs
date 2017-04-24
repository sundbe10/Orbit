using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Find("dying").GetComponent<Text>().text = "You lived "+ GameManager.years + " million years";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
