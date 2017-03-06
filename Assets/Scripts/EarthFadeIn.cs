using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class EarthFadeIn : MonoBehaviour, ITrackableEventHandler {
	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		Debug.Log("<b>EarthFadeIn</b>:Status changed!");
		if (newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			Camera.main.transform.Find("BackgroundPlane").gameObject.SetActive(false);
			Camera.main.clearFlags = CameraClearFlags.Skybox;

		}
		else {
			
		}
	}

	// Use this for initialization
	void Start () {
		TrackableBehaviour track =  GetComponent<TrackableBehaviour>();
		if (track != null) {
			track.RegisterTrackableEventHandler(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
