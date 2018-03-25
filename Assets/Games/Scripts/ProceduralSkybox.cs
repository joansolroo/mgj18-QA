using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSkybox : MonoBehaviour {

    [SerializeField] Material material;
    [SerializeField] float r = 100;
    // Use this for initialization
    void Start() {
        for (int a = 0; a < 360; a += Random.Range(8,20))
        {
            for (int b = 0; b < 360; b += Random.Range(8, 20))
            {
                GameObject go =  GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = this.transform;
                go.transform.localPosition = new Vector3(r * Mathf.Sin(a) * Mathf.Cos(b), r * Mathf.Sin(a) * Mathf.Sin(b), r * Mathf.Cos(a));
                go.transform.localScale = Vector3.one * 0.1f;
                go.GetComponent<MeshRenderer>().material = material;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
