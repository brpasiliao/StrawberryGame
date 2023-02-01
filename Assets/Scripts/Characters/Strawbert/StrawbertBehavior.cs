using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertBehavior : MonoBehaviour, ILaunchable {
    public StrawbertAnimation strawbertA;
    public Stem stem;
    public Flower flower;

    public Vector3 PosOG { get; set; }
    public bool BeingLaunched { get; set; }
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    public bool inSpores = false;   // ??? cant be a variable for every scenario

    public bool canMove = true;
    public float speed;
    private Vector3 destination;

    private void OnEnable() {
        EventBroker.onSetCanMove += SetCanFunction;
    }

    private void OnDisable() {
        EventBroker.onSetCanMove -= SetCanFunction;
    }

    void Update() {
        if (canMove) Walk();
    }

    public void SetCanFunction(bool can) {
        if (can) {
            SetCanMove(true);
            strawbertA.SetCanAnimate(true);
            stem.SetCanRotate(true);
            flower.SetCanReach(true);
        } else {
            SetCanMove(false);
            strawbertA.SetCanAnimate(false);
            stem.SetCanRotate(false);
            flower.SetCanReach(false);
        }
    }

    public void SetCanMove(bool can) {
        if (can) canMove = true;
        else canMove = false;
    }

    void Walk() {
        destination = new Vector3(Input.GetAxisRaw(PlayerInput.HORIZONTAL), Input.GetAxisRaw(PlayerInput.VERTICAL), 0);
        destination.Normalize();
        transform.Translate(destination.x*speed, destination.y*speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Tags.WALLCOLLISION) || collision.gameObject.CompareTag(Tags.OBJECT)) {
            HittingSomething = true;
        } else if (collision.gameObject.CompareTag(Tags.RIVERCOLLISION) && BeingLaunched) {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Tags.WALLCOLLISION) || collision.gameObject.CompareTag(Tags.OBJECT))
            HittingSomething = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.transform.parent != null && collider.transform.parent.GetComponent<Mushroom>() != null) {
            if (BeingLaunched) inSpores = true;
            // else lose life
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag(Tags.RIVERCOLLISION) && BeingLaunched) {
            InRiver = false;
        } else if (BeingLaunched && collider.transform.parent != null && collider.transform.parent.GetComponent<Mushroom>() != null) {
            inSpores = false;
        }
    }

    public void ResetObject() {
        if (InRiver) {
            transform.position = PosOG;
            InRiver = false;
        } else if (inSpores) {
            transform.position = PosOG;
            // lose life
            inSpores = false;
        }

        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        BeingLaunched = false;
    }
}
