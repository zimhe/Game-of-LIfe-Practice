using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Collide_Practice : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("touched");
        //other.gameObject.GetComponent<CA_Cube_Practice >().SetState(0);
    }

   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    }
}
