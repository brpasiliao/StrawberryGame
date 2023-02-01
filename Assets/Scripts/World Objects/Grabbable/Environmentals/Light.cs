using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Environmental, ILaunchable {
    public Vector3 PosOG { get; set; }
    public bool BeingLaunched { get; set; }
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    private Mushroom mushroom;  // does this have to be referenced???

    protected override void Primary() {
        transform.SetParent(flower.transform);
        StartCoroutine(flower.Retract());
    }

    protected override void Secondary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Reach");
    }

    protected override void Cancel() {
        StartCoroutine(flower.Retract());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Tags.WALLCOLLISION) || collision.gameObject.CompareTag(Tags.OBJECT)) {
            HittingSomething = true;
        } else if (collision.gameObject.CompareTag(Tags.RIVERCOLLISION) && BeingLaunched) {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Tags.WALLCOLLISION) || collision.gameObject.CompareTag(Tags.OBJECT)) {
            HittingSomething = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<Mushroom>() != null) {
            mushroom = collider.gameObject.GetComponent<Mushroom>();
            if (BeingLaunched) {
                mushroom.isMush = true;
            } else {
                mushroom.GetMushed();
                Destroy(gameObject);
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag(Tags.RIVERCOLLISION) && BeingLaunched) {
            InRiver = false;
        } else if (BeingLaunched && collider.gameObject.GetComponent<Mushroom>() != null) {
            mushroom.isMush = false;
        }
    }

    public void ResetObject() {
        if (InRiver) {
            transform.position = PosOG;
            InRiver = false;
        } else if (mushroom.isMush) {
            mushroom.GetMushed();
            Destroy(gameObject);
        }
        
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        BeingLaunched = false;
    }
}
