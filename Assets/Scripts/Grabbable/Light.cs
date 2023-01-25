using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Environmental, ILaunchable {
    public bool BeingLaunched { get; set; }
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    new void Start() { base.Start(); }

    void Update() {}

    protected override void Primary() {
        transform.SetParent(flower.transform);
        StartCoroutine(flower.Retract());
    }

    protected override void Secondary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Reach");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = true;
        }
        else if (collision.gameObject.tag == Tags.RIVERCOLLISION && BeingLaunched) {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == Tags.RIVERCOLLISION && BeingLaunched) {
            InRiver = false;
        }
    }

    public void ResetObject() {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
