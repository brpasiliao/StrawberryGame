using UnityEngine;
using TMPro;

public class CollectibleText : MonoBehaviour {
    public int resourceCounter = 0, valuableCounter = 0;

    public bool resource, valuable;

    TextMeshProUGUI score;

    private void OnEnable() {
        EventBroker.onResourceCollection += UpdateResource;
        EventBroker.onValuableCollection += UpdateValuable;
    }

    private void OnDisable() {
        EventBroker.onResourceCollection -= UpdateResource;
        EventBroker.onValuableCollection -= UpdateValuable;
    }

    // Start is called before the first frame update
    void Start() {
        score = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateResource() {
        if (resource && !valuable) {
            score.text = "Resource: " + ++resourceCounter;
        }
    }

    public void UpdateValuable() {
        if (!resource && valuable){
            score.text = "Valuable " + ++valuableCounter;
        }
    }
}
