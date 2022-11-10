using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertMovement : MonoBehaviour {
    public Flower flower;
    public float speed;

    public SpriteRenderer sr;
    public Sprite n;
    public Sprite s;
    public Sprite e;
    public Sprite w;
    public Sprite ne;
    public Sprite nw;
    public Sprite se;
    public Sprite sw;

    void Start() {}

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
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0)
            sr.sprite = ne;
        else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0)
            sr.sprite = nw;
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0)
            sr.sprite = se;
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0)
            sr.sprite = sw;
        else if (Input.GetAxisRaw("Vertical") > 0)
            sr.sprite = n;
        else if (Input.GetAxisRaw("Vertical") < 0)
            sr.sprite = s;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            sr.sprite = e;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            sr.sprite = w;
    }
}
