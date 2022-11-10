using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertAnimation : MonoBehaviour {
    public Sprite up;
    public Sprite down;
    public Sprite side;

    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetAxisRaw("Vertical") > 0) sr.sprite = up;
        if (Input.GetAxisRaw("Vertical") < 0) sr.sprite = down;
        if (Input.GetAxisRaw("Horizontal") != 0) {
            sr.sprite = side;
            if (Input.GetAxisRaw("Horizontal") > 0) sr.flipX = false;
            if (Input.GetAxisRaw("Horizontal") < 0) sr.flipX = true;
        }
    }
}
