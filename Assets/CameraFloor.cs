using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFloor : MonoBehaviour {

  private Collider collider;

  void Setup() {
    collider = gameObject.GetComponent<Collider>();
  }

  void OnCollisionEnter(Collision collision)
  {

    if (collision.gameObject.tag == "txPrism")
    {
      Physics.IgnoreCollision(collision.collider, collider);
    }

  }
}
