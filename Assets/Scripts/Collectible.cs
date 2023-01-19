using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == Tags.PLAYER && this.gameObject.tag == Tags.RESOURCE) {
            EventBroker.CallResourceCollection();
            Destroy(gameObject);
        } else if (collision.tag == Tags.PLAYER && this.gameObject.tag == Tags.VALUABLE) {
            EventBroker.CallValuableCollection();
            Destroy(gameObject);
        }
    }
}
