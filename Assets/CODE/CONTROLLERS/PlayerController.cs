using UnityEngine;

/// <summary>
/// This class is responsible for all keyboard and mouse input.
/// </summary>
public class PlayerController : MonoBehaviour {

    KeyCode[] keycodes;
    SimonManager simon;


    void Start() {
        simon = RootManager.instance.simonManager;
        keycodes = simon.keycodes;
    }

    void Update() {
        if (simon.gameState != GameState.Paused) {
            KeyboardUpdate();
            MouseUpdate();
        }
    }

    void KeyboardUpdate() {
        for (int i = 0; i < keycodes.Length; i++)
            if (Input.GetKeyDown(keycodes[i]))
                simon.Guess(keycodes[i]);
    }

    void MouseUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
                if (hit.collider.tag == Tag.CLICKABLE)
                    hit.transform.gameObject.GetComponent<IClickable>().Click();

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
        }
    }
}
