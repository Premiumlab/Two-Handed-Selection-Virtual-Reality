using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour {
    public GameObject box;
    public GameObject myself;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Debug.Log(transform.position + " box.position: " + box.transform.position + " box.localScale/2: " + box.transform.localScale/2);

        if(transform.position.x < box.transform.position.x + box.transform.localScale.x / 2
            && transform.position.x > box.transform.position.x - box.transform.localScale.x / 2
            && transform.position.y < box.transform.position.y + box.transform.localScale.y / 2
            && transform.position.y > box.transform.position.y - box.transform.localScale.y / 2
            && transform.position.z < box.transform.position.z + box.transform.localScale.z / 2
            && transform.position.z > box.transform.position.z - box.transform.localScale.z / 2)
        {
            // Debug.Log("I am color changing");

            if (myself.tag.Equals("select"))
            {
                myself.GetComponent<Renderer>().material.color = Color.red;
            }

            if (myself.tag.Equals("unselect"))
            {
                myself.GetComponent<Renderer>().material.color = Color.yellow;
            }

        }
        else
        {
            if (myself.tag.Equals("select"))
            {
                // blue: 0, 0, 255
                myself.GetComponent<Renderer>().material.color = Color.blue;
            }

            if (myself.tag.Equals("unselect"))
            {
                // dull yellow: 245, 255, 6
                myself.GetComponent<Renderer>().material.color = new Color(245.0f/255f, 180.0f/255f, 6.0f/255f);
            }
        }

    }
}
