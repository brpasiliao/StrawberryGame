using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertMovement : MonoBehaviour {
    public Flower flower;
    public float speed;

    void Start() {}

    void Update() {
        if (!flower.reaching) Walk();
    }

    void Walk() {
        transform.Translate(Input.GetAxisRaw("Horizontal")*speed, Input.GetAxisRaw("Vertical")*speed, 0);
    }
}
