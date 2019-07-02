using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZInput : MonoBehaviour {
    public float zoom = 2f;
    float smooth = 5.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition;
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (transform.localPosition.z < -7)
            {
                newPosition = transform.localPosition + new Vector3(0f, 0f, -zoom);
                transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, smooth * Time.deltaTime);
                //transform.localPosition += new Vector3(0f, 0f, -zoom);
            }
            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (transform.localPosition.z > -23)
            {
                newPosition = transform.localPosition + new Vector3(0f, 0f, zoom);
                transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, smooth * Time.deltaTime);
                //transform.localPosition += new Vector3(0f, 0f, zoom);
            }
        }
    }
}
