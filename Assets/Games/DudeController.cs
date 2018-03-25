using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour {

    [SerializeField] float rotationSpeed = 5;
    [SerializeField] float movementSpeed = 5;
    // Use this for initialization
    void Start () {
		
	}

    [SerializeField] Vector2 speed=Vector2.zero;
	// Update is called once per frame
	void Update () {
        float dr = Input.GetAxis("Horizontal");
        float dx = Input.GetAxis("Vertical");
        speed.x = dx; speed.y = dr;
        Debug.Log("dr:" + dx + " dy:" + dx);
        Vector3 rotation = gameObject.transform.localEulerAngles;
        rotation.y += dr*rotationSpeed*Time.deltaTime;
        rotation.z = Mathf.Lerp(0,Mathf.Sin(Time.time*10) * 35/2, Mathf.Abs(dx));
        gameObject.transform.position = gameObject.transform.TransformPoint(0, 0, -dx * movementSpeed*Time.deltaTime);
        gameObject.transform.localEulerAngles = rotation;
	}
}
