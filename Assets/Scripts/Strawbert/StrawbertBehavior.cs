using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertBehavior : MonoBehaviour {
    public Flower flower;

    public float speed;
    private Vector3 destination;

    void Start() {}

    void Update() {
        if (!flower.reaching) Walk();
    }

    void Walk() {
        destination = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        destination.Normalize();
        transform.Translate(destination.x*speed, destination.y*speed, 0);
    }
}
