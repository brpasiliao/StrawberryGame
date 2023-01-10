using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertAnimation : MonoBehaviour {
    private SpriteRenderer sr;
    public Sprite n;
    public Sprite s;
    public Sprite e;
    public Sprite w;
    public Sprite ne;
    public Sprite nw;
    public Sprite se;
    public Sprite sw;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!Flower.reaching && !SpringLeaf.launching) 
            Animation();
    }

    void Animation() {
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
