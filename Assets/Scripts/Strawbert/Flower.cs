using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
    public GameObject strawbert;

    public float posOG;         // original position of head
    public float posStep;       // how long each movement is
    public float posMax;        // the farthest length it can go
    public float posBack;       // multiplier to retract faster

    public static bool reaching = false;
    public bool grabbing = false;
    public bool grappling = false;
    private bool disabled = false;

    void Start() {}

    void Update() {
        if (!reaching && !SpringLeaf.launching && Input.GetKeyDown("space"))
            StartCoroutine("Reach");
    }

    IEnumerator Reach() {
        Debug.Log("reach");

        GetComponent<BoxCollider2D>().enabled = true; // can grab multiple?
        reaching = true;

        while (transform.localPosition.x < posMax) {
            transform.Translate(posStep, 0, 0);
            transform.localPosition = new Vector2(transform.localPosition.x, 0);
            yield return null;
        }

        Release();

        StartCoroutine("Retract"); 
    }

    public IEnumerator Retract() {
        Debug.Log("retract");
        grabbing = false;

        while (transform.localPosition.x > posOG+(posStep*posBack)) {
            transform.Translate(-posStep*posBack, 0, 0);
            transform.localPosition = new Vector2(transform.localPosition.x, 0);
            if (grappling) {
                // strawbert.transform.Translate(-posStep*posBack*2, 0, 0);
                // Debug.Log("strawbert: " + strawbert.transform.position + "; flower: " + transform.position);
                strawbert.transform.position = Vector2.MoveTowards(strawbert.transform.position, transform.position, posStep*posBack);
            }
            yield return null;
        }

        Release();
        grappling = false;
        reaching = false;
        transform.localPosition = new Vector2 (posOG, transform.localPosition.y);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // public IEnumerator Grapple() {
    //     while (transform.localPosition.x > posOG) {
    //         strawbert.transform.Translate(-posStep*posBack, 0, 0);
    //         yield return null;
    //     }
    // }

    private void Release() {
        if (transform.childCount > 0) {
            Transform child = transform.GetChild(0);
            transform.GetChild(0).SetParent(child.GetComponent<Environmental>().parentOG);
        }
    }
}