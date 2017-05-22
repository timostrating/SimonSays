using System.Collections;
using UnityEngine;

public class ButtonController : MonoBehaviour, IClickable, IHighlightable {

    [SerializeField] KeyCode key;
    [SerializeField] GameObject lightObject;

    SimonManager simon;
    float lightTogleSpeed = 0.5f;


    void Awake() {
        simon = RootManager.instance.simonManager;
        gameObject.tag = Tag.CLICKABLE;
        lightObject.SetActive(false);
    }

    public void Click() {
        simon.Guess(key);
    }

    public void Highlight() { StartCoroutine(IEHighlight()); }
    IEnumerator IEHighlight() {
        lightObject.SetActive(true);
        yield return new WaitForSeconds(lightTogleSpeed);
        lightObject.SetActive(false);
        yield return null;
    }
}
