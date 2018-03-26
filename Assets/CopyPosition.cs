using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour {

    [SerializeField] Transform other;
    [SerializeField] float speed = 1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(other.transform.position-this.transform.position,Vector3.up),10);
        this.transform.position = Vector3.MoveTowards(this.transform.position,other.transform.position,speed*Vector3.Distance(this.transform.position, other.transform.position)*Time.deltaTime);
      
    }
}
