using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	public float speed = 10f;
	public bool enableSpin;


	// Update is called once per frame
	void Update () {
		if (enableSpin) {
			transform.Rotate (Vector3.up, -1 * speed * Time.deltaTime);
		}


	}
}
