using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

	private static GravityManager instance = null;
	private static List<GravityBehavior> gravityObjects;
	public static float G = 6.67408E-11f;

	public static GravityManager SharedInstance {
		get {
			if (instance == null) {
				instance = new GravityManager();
			}
			return instance;
		}
	}

	void Awake() {
		gravityObjects = new List<GravityBehavior>();
	}

	public static void RegisterGravityObject(GravityBehavior obj) {
		if (!gravityObjects.Contains(obj))
		{
			gravityObjects.Add(obj);
		}
	}

	public static void UnregisterGravityObject(GravityBehavior obj) {
		if (gravityObjects.Contains(obj))
		{
			gravityObjects.Remove(obj);
		}
	}

	void Update() {
		foreach(GravityBehavior giveObj in gravityObjects)
		{
			foreach(GravityBehavior receiveObj in gravityObjects)
			{
				if (giveObj != receiveObj)
					ApplyPullForce(giveObj,receiveObj);
			}
		}
	}
		
	void ApplyPullForce(GravityBehavior obj1, GravityBehavior obj2)
	{
		Vector2 pos1 = obj1.GetPosition();
		Vector2 pos2 = obj2.GetPosition();

		float radius = Vector2.Distance(pos1, pos2);

		Vector2 force = (pos1 - pos2).normalized * 10E10f * (G * obj1.mass * obj2.mass) / (radius * radius);

		obj2.body.AddForce(force);
	}
}
