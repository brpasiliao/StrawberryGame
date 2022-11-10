using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
    public Flower flower;
    public int direction = 1;

    void Start() {}

    void Update() {
        if (!flower.reaching) {
            if (Input.GetAxisRaw("Vertical") > 0) {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                direction = 1;
            }
            if (Input.GetAxisRaw("Vertical") < 0) {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                direction = 2;
            }
            if (Input.GetAxisRaw("Horizontal") > 0) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                direction = 3;
            }
            if (Input.GetAxisRaw("Horizontal") < 0) {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                direction = 4;
            }
        }
    }
}
