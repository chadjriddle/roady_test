﻿using UnityEngine;

public class Tire : MonoBehaviour {

	public float RestingWeight { get; set; }
	public float ActiveWeight { get; set; }
	public float Grip { get; set; }
	public float FrictionForce { get; set; }
	public float AngularVelocity { get; set; }
	public float Torque { get; set; }

	public float Radius = 0.5f;

	float TrailDuration = 5;
	bool TrailActive;
	GameObject Skidmark;

    private GameObject SkidMarkPrefab;

    public void Start()
    {
        SkidMarkPrefab = Resources.Load("Skidmark") as GameObject;
    }


	public void SetTrailActive(bool active) {
		if (active && !TrailActive) {
			// These should be pooled and re-used
			Skidmark = Instantiate(SkidMarkPrefab);

            Skidmark.transform.parent = this.transform;
            Skidmark.transform.localPosition = Vector2.zero;

            //Fix issue where skidmarks draw at 0,0,0 at slow speeds
            Skidmark.GetComponent<TrailRenderer>().Clear();
            Skidmark.GetComponent<TrailRenderer> ().time = TrailDuration;
			Skidmark.GetComponent<TrailRenderer> ().sortingOrder = 0;


        } else if (!active && TrailActive) {			
			Skidmark.transform.parent = null;
			GameObject.Destroy (Skidmark.gameObject, TrailDuration); 
		}
		TrailActive = active;
	}

}
