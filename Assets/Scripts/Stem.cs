using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
    public Flower flower;

    void Start() {}

    void Update() {
        if (!flower.reaching) {
            if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0) // ne
                transform.rotation = Quaternion.Euler(0, 0, 45);
            else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0) // nw
                transform.rotation = Quaternion.Euler(0, 0, 135);
            else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0) // se
                transform.rotation = Quaternion.Euler(0, 0, -45);
            else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0) // sw
                transform.rotation = Quaternion.Euler(0, 0, -135);
            else if (Input.GetAxisRaw("Vertical") > 0) // n
                transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (Input.GetAxisRaw("Vertical") < 0) // s
                transform.rotation = Quaternion.Euler(0, 0, -90);
            else if (Input.GetAxisRaw("Horizontal") > 0) // e
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (Input.GetAxisRaw("Horizontal") < 0) // w
                transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
