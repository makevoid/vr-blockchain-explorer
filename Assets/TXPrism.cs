using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TXPrism : MonoBehaviour {

  private float speedRot = 1; // rotation speed

	// Use this for initialization
	void Start () {
		
	}

  void Update()
  {
    transform.Rotate(Vector3.forward * Time.deltaTime * speedRot);
    transform.Rotate(Vector3.left * Time.deltaTime * speedRot);
  }
}
