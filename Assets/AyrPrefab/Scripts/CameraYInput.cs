using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYInput : MonoBehaviour {
    //private GameObject[] gameObjects;
    public GameObject focusObject;

    float smooth = 30.0f;
    int degrees = 10;

	//void Start () {
	//	//gameObjects = GameObject.FindGameObjectsWithTag("PlayerCube");
	//}
    
	void Update () {

        Vector3 newPosition = focusObject.transform.position;
        //transform.position = Vector3.Lerp(transform.localPosition, newPosition, smooth * Time.deltaTime); //smooth camera movement
        transform.position = newPosition;
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(focusObject.transform.position, Vector3.up, Input.GetAxis("Mouse X") * degrees);
        }
	}
}
