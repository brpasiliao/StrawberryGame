using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertBehavior : MonoBehaviour, IThrowable {
    public float speed;
    private Vector3 destination;
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    void Start() {}

    void Update() {
        if (!Flower.reaching && !SpringLeaf.launching)
            Walk();
    }

    void Walk() {
        destination = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        destination.Normalize();
        transform.Translate(destination.x*speed, destination.y*speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = true;
        }
        else if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching) {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching) {
            InRiver = false;
        }
    }

    public void ThrowingObject() {
        enabled = false;
    }

    public void ResetObject() {
        enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
    }
}
