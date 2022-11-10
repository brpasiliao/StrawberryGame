using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertMovement : MonoBehaviour {
    public Flower flower;
    public float speed;

    private SpriteRenderer sr;
    public Sprite up;
    public Sprite down;
    public Sprite side;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!flower.reaching) {
            Walk();
            Animations();
        }
    }

    void Walk() {
        transform.Translate(Input.GetAxisRaw("Horizontal")*speed, Input.GetAxisRaw("Vertical")*speed, 0);
    }

    void Animations() {
        if (Input.GetAxisRaw("Vertical") > 0) sr.sprite = up;
        if (Input.GetAxisRaw("Vertical") < 0) sr.sprite = down;
        if (Input.GetAxisRaw("Horizontal") != 0) {
            sr.sprite = side;
            if (Input.GetAxisRaw("Horizontal") > 0) sr.flipX = false;
            if (Input.GetAxisRaw("Horizontal") < 0) sr.flipX = true;
        }
    }
}
