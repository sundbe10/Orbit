using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CameraMover : MonoBehaviour {

	float speed = 0.5f;

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") < 0)
			this.transform.position -= Vector3.right*speed;
		else if (Input.GetAxis("Horizontal") > 0)
			this.transform.position += Vector3.right*speed;

		if (Input.GetAxis("Vertical") < 0)
			this.transform.position -= Vector3.up*speed;
		else if (Input.GetAxis("Vertical") > 0)
			this.transform.position += Vector3.up*speed;
	}
}
