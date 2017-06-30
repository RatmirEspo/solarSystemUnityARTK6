using UnityEngine;
using System.Collections;

public class distanceLine : AAREventReceiver {

	LineRenderer line;
	ARTrackable marker1;
	ARTrackable marker2;
	public GameObject eventReceiver;
	float alpha =1;
	public float fadingSpeed = 0.8f;
	public string lineTag;
    public string marker1Tag;
	public string marker2Tag;


	// Use this for initialization
	void Start () {
		line = GameObject.FindGameObjectWithTag (lineTag).GetComponent<LineRenderer>();

		//Color color = new Color(1f,1f,1f,1f);
		//line.SetColors (color,color);
		//line.material = new Material(Shader.Find("Particles/Additive"));
		line.SetWidth(0.0f, 0.0f);
	}

    override public void OnMarkerFound(ARTrackable marker)
	{
        if (marker.trackableTag.Equals(marker1Tag)) {
			marker1 = marker;
		} else {
			marker2 = marker;
		}
		drawLine (marker);
	}

    override public void OnMarkerLost(ARTrackable marker){
        if (marker.trackableTag.Equals(marker1Tag)) {
			marker1 = null;
		} else {
			marker2 = null;
		}
		line.SetWidth (0f, 0f);
	}

	override public void OnMarkerTracked(ARTrackable marker){

		drawLine (marker);

	}

	void NotInRange(){
		Color color = new Color (0.53f,0.05f,0.11f,1f);
		line.SetColors (color,color);
		alpha = 1;
	}

	void InRange(){
		Color color = new Color(0.19f,0.82f,0.09f,alpha);
		line.SetColors (color,color);
		alpha = alpha - Time.deltaTime * fadingSpeed;
	}

	private void drawLine(ARTrackable marker){
		//Make sure that the tracked marker is one of the markers we are interested in and that we have the other one already loaded
        if (marker1 != null && marker2 != null && (marker.trackableTag.Equals (marker1Tag) || marker.trackableTag.Equals (marker2Tag))) {
			Vector3 startPosition = marker1.transform.position;
			line.SetPosition (0, startPosition);

			Vector3	targetPosition = marker2.transform.position;
			line.SetPosition (1, targetPosition);
			line.SetWidth (0.01f, 0.01f);
            float distance = Vector3.Distance (targetPosition, startPosition);
            eventReceiver.BroadcastMessage ("OnLineChange", distance, SendMessageOptions.DontRequireReceiver);
            Debug.LogWarning ("Distance:" + distance);
		}
	}
}
