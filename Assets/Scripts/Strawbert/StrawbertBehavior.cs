using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertBehavior : MonoBehaviour {
    public float speed;
    public bool hittingSomething;
    public bool inRiver;
    private Vector3 destination;

    private void OnEnable() {
    }

    private void OnDisable() {
    }

    void Start() {}

    void Update() {
        if (!Flower.reaching && !SpringLeaf.launching)
            Walk();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            hittingSomething = true;
        } else if(collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching) {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            inRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            hittingSomething = false;
        } else if (collision.gameObject.tag == Tags.RIVERCOLLISION) {
            //inRiver = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching) {
            inRiver = false;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching)
        {
            inRiver = false;
        }
    } */

    void Walk() {
        destination = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        destination.Normalize();
        transform.Translate(destination.x*speed, destination.y*speed, 0);
    }

}
