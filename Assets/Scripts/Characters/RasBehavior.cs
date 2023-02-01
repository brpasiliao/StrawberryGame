using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RasBehavior : MonoBehaviour, ILaunchable {
    StrawbertBehavior strawbertB;

    public bool withStrawbert = true;
    public float waitTeleport;

    public Vector3 PosOG { get; set; }
    public bool BeingLaunched { get; set; }
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    private void Start() {
        strawbertB = GameObject.FindWithTag(Tags.PLAYER).GetComponent<StrawbertBehavior>();
    }

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

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Tags.RIVERCOLLISION) && BeingLaunched)
            InRiver = false;
    }

    public void ResetObject() {
        if (InRiver) {
            transform.position = PosOG;
            InRiver = false;
        }

        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        BeingLaunched = false;
    }

    public IEnumerator TeleportToStrawbert() {
        yield return new WaitForSeconds(waitTeleport);
        transform.position = new Vector2(strawbertB.transform.position.x-1, strawbertB.transform.position.y);
        withStrawbert = true;
    }
}
