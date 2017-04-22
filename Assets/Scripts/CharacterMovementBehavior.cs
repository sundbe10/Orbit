using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementBehavior : MonoBehaviour {

	float acceleration = 1f;
	float maxjumpForce = 4000f;
	float pushForce = 50f;
	float jumpForce;
	float angVelocity;
	Vector2 gravVelocity;
	float maxSpeed = 5f;
	[HideInInspector] public bool isGrounded;
	[HideInInspector] public GameObject attachedPlanet;
	float characterGravityConstant = 10E-5f;

	// Use this for initialization
	void Awake () {
		angVelocity = 0;
		gravVelocity = Vector2.zero;
		isGrounded = false;
		jumpForce = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		// Jumping
		if (Input.GetButton("Jump") && jumpForce > 0f)
		{
			Vector2 jumpVec = (transform.position - attachedPlanet.transform.position) * jumpForce;
			Fall(jumpVec);
			jumpForce = Mathf.Lerp(jumpForce,0,15f*Time.deltaTime);
			if (isGrounded)
			{
				Debug.Log("Exit!");
				isGrounded = false;
				transform.parent = null;
			}
		}

		// Walking
		if (Input.GetAxis("Horizontal") < 0 && angVelocity >  -maxSpeed)
		{
			angVelocity = Mathf.Lerp(angVelocity, -maxSpeed, acceleration*Time.deltaTime);
		}
		else if (Input.GetAxis("Horizontal") > 0 && angVelocity < maxSpeed)
		{
			angVelocity = Mathf.Lerp(angVelocity, maxSpeed, acceleration*Time.deltaTime);
		}
		else if (angVelocity != 0)
			angVelocity = Mathf.Lerp(angVelocity, 0f, acceleration*Time.deltaTime);
		else
			angVelocity = 0f;

		float radius = Vector2.Distance((Vector2)transform.position, attachedPlanet.transform.position);
		transform.RotateAround(attachedPlanet.transform.position, Vector3.forward, -angVelocity/(radius*radius));

		// Auto-turning
		if (attachedPlanet != null)
		{
			Vector3 distanceVec = transform.position - attachedPlanet.transform.position;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward,(distanceVec)), Mathf.Pow(3/distanceVec.magnitude,3f));
		}

		// Blowing
		if (Input.GetButton("Fire3") && isGrounded)
		{
			Vector2 force = (attachedPlanet.transform.position - transform.position).normalized*pushForce;
			attachedPlanet.GetComponent<Rigidbody2D>().AddForce(force);
		}
	}

	public void Fall(Vector2 force)
	{
		gravVelocity += force*characterGravityConstant;
		Vector3 newPos = (Vector2)transform.position + gravVelocity*Time.deltaTime;
		transform.position = newPos;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Planet")
		{
			gravVelocity = Vector2.zero;
			isGrounded = true;
			jumpForce = maxjumpForce;
			transform.parent = attachedPlanet.transform;
		}
	}
}
