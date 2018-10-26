using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    // back top point
    public GameObject point;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - point.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // transform.localScale += point.transform.position + offset;
        // transform.localScale += new Vector3(0.1F, 0, 0);
	}
}
