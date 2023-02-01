using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : Environmental {
    bool onEdge = false;

    protected override void Primary() {
        StartCoroutine(flower.Grapple());
    }

    protected override void Secondary() {
        Debug.Log("heavy secondary");
        StartCoroutine(PushOrPull());
    }

    protected override void Cancel() {
        flower.strawbertB.transform.SetParent(null);
        flower.transform.SetParent(flower.strawbertB.stem.transform);
        StartCoroutine(flower.Retract());
    }

    private IEnumerator PushOrPull() {
        while (!Input.GetKeyDown(KeyCode.UpArrow) &&
            !Input.GetKeyDown(KeyCode.DownArrow) &&
            !Input.GetKeyDown(KeyCode.LeftArrow) &&
            !Input.GetKeyDown(KeyCode.RightArrow))
                { yield return null; }

        if (!onEdge &&
            (Input.GetKeyDown(KeyCode.UpArrow) &&
            flower.strawbertB.stem.direction == Directions.NORTH) ||
            (Input.GetKeyDown(KeyCode.DownArrow) &&
            flower.strawbertB.stem.direction == Directions.SOUTH) ||
            (Input.GetKeyDown(KeyCode.LeftArrow) &&
            flower.strawbertB.stem.direction == Directions.WEST) ||
            (Input.GetKeyDown(KeyCode.RightArrow) &&
            flower.strawbertB.stem.direction == Directions.EAST)) {
                transform.SetParent(flower.transform);
                flower.StartCoroutine("Reach");
        } else if (!onEdge &&
            (Input.GetKeyDown(KeyCode.UpArrow) &&
            flower.strawbertB.stem.direction == Directions.SOUTH) ||
            (Input.GetKeyDown(KeyCode.DownArrow) &&
            flower.strawbertB.stem.direction == Directions.NORTH) ||
            (Input.GetKeyDown(KeyCode.LeftArrow) &&
            flower.strawbertB.stem.direction == Directions.EAST) ||
            (Input.GetKeyDown(KeyCode.RightArrow) &&
            flower.strawbertB.stem.direction == Directions.WEST)) {
                transform.SetParent(flower.transform);
                StartCoroutine(flower.Retract());
        } else {
            StartCoroutine(flower.Retract());
        }
    }

    // private void OnTriggerEnter2D(Collider2D collider) {
    //     if (collider.GetComponent<RockScratch>() != null) {
    //         onEdge = true;

    //         transform.SetParent(parentOG);
    //         flower.StopCoroutine("Reach");
    //         StartCoroutine(flower.Retract());
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collider) {
    //     if (collider.GetComponent<RockScratch>() != null) {
    //         onEdge = false;

    //         transform.SetParent(parentOG);
    //         flower.StopCoroutine("Reach");
    //         StartCoroutine(flower.Retract());
    //     }
    // }
}
