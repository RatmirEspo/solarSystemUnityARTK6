using UnityEngine;
using System.Collections;

public class rotateAroundObject : MonoBehaviour {
	Vector3 rotationMask = new Vector3(0f, 0f, 1f);
	public float rotationSpeed = 15f;
	private float distance = 0;
	public GameObject eventReceiver;
	public string rotationObject;
	public float validDistanceMin;
	public float validDistanceMax;
	public float radius = 2.0f;
	private GameObject rotateObject;

	void OnLineChange(float dist){
		this.distance = dist;
		rotateObject = GameObject.FindWithTag(rotationObject);
		if (rotateObject != null && distance > validDistanceMin && distance < validDistanceMax) {
			startRotation ();
			if (eventReceiver != null) {
				eventReceiver.BroadcastMessage ("InRange");
			}
		} else {
			if (eventReceiver != null) {
				eventReceiver.BroadcastMessage ("NotInRange");
			}
		}
	}

	private void startRotation(){
		transform.RotateAround (rotateObject.transform.position, rotationMask, rotationSpeed * Time.deltaTime);

		Vector3 desiredPosition = (transform.position - rotateObject.transform.position).normalized * radius + rotateObject.transform.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 1);

	}

	// Use this for initialization
	void Start () {
		rotateObject = GameObject.FindWithTag(rotationObject);
//		if (rotateObject != null) {
//			Transform center = rotateObject.transform;
//            Debug.LogWarning ("RotateObject.transform" + center.position);
//			transform.position = (transform.position - center.position).normalized * radius + center.position;
//		}
	}

	// Update is called once per frame
	void Update () {

	}
}
