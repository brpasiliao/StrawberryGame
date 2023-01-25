using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RasBehavior : MonoBehaviour, ILaunchable {
    public Transform strawbert;

    public bool withStrawbert = true;
    public float waitTeleport;

    public bool BeingLaunched { get; set; }
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    private void OnEnable() {
        EventBroker.onSetCanMove += SetCanMove;
    }

    private void OnDisable() {
        EventBroker.onSetCanMove -= SetCanMove;
    }

    void SetCanMove(bool can) {
        if (can) GetComponent<IAstarAI>().canMove = true;
        else GetComponent<IAstarAI>().canMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = true;
        } else if (collision.gameObject.tag == Tags.RIVERCOLLISION && BeingLaunched) {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT)
            HittingSomething = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == Tags.RIVERCOLLISION && BeingLaunched) 
            InRiver = false;
    }

    public void ResetObject() {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public IEnumerator TeleportToStrawbert() {
        yield return new WaitForSeconds(waitTeleport);
        transform.position = new Vector2(strawbert.position.x-1, strawbert.position.y);
        withStrawbert = true;
    }
}
