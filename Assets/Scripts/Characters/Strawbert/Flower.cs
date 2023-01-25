using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour {
    public StrawbertBehavior strawbertB;

    public float posOG;         // original position of head
    public float posStep;       // how long each movement is
    public float posMax;        // the farthest length it can go
    public float posBack;       // multiplier to retract faster
    public float lerpSmoothing;
    public float lerpCutoff;
    public bool canReach = true;

    public bool reaching = false;   // animation state
    public bool grabbing = false;
    public bool grappling = false;

    void Update() {
        if (canReach && Input.GetKeyDown("space"))
            StartCoroutine("Reach");
    }

    public void SetCanReach(bool can) {
        if (can) canReach = true;
        else canReach = false;
    }

    public IEnumerator Reach() {
        GetComponent<BoxCollider2D>().enabled = true;
        strawbertB.SetCanFunction(false);
        reaching = true; 

        Vector3 end = new Vector3(posMax, 0, 0);
        while (transform.localPosition.x < posMax-lerpCutoff) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, end, lerpSmoothing * Time.deltaTime);            // transform.Translate(posStep, 0, 0);
            yield return null;
        }

        Release();

        StartCoroutine(Retract()); 
        AstarPath.active.Scan();
    }

    public IEnumerator Retract() {
        grabbing = false;

        Vector3 end = new Vector3(posOG, 0, 0);
        while (transform.localPosition.x > posOG+lerpCutoff) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, end, lerpSmoothing * Time.deltaTime);
            // if (grappling) {
                // strawbert.transform.Translate(-posStep*posBack*2, 0, 0);
                // Debug.Log("strawbert: " + strawbert.transform.position + "; flower: " + transform.position);
                // strawbert.transform.position = Vector2.MoveTowards(strawbert.transform.position, transform.position, posStep*posBack);
            // }
            yield return null;
        }

        if (transform.GetChild(0).GetComponent<Environmental>() != null)
            Release();
        else if (transform.GetChild(0).GetComponent<Collectible>() != null)
            transform.GetChild(0).GetComponent<Collectible>().Collect();

        strawbertB.SetCanFunction(true);
        reaching = false; 
        grappling = false;
        transform.localPosition = new Vector2 (posOG, transform.localPosition.y);
        GetComponent<BoxCollider2D>().enabled = false;
        AstarPath.active.Scan();
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