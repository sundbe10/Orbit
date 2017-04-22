using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = transform.Find("Score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		text.text = GameManager.years + " Million Years";
	}
}
