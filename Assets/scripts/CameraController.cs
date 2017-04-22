using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       
	public int narrowCameraFoV;
	public int wideCameraFoV;

	private Vector3 offset;         
	private bool starNearby = false;
	private Camera mainCamera;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GetComponent<Camera>();
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		transform.position = player.transform.position + offset;
		mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, starNearby ? wideCameraFoV : narrowCameraFoV, Time.deltaTime);
	}

	void OnTriggerStay2D(Collider2D collider){
		if(collider.tag == "Star"){
			starNearby = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Star"){
			starNearby = false;
		}
	}

}
