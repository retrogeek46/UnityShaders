using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolveCamera : MonoBehaviour {

    float speed = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, speed);
	}
}
