using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonController : MonoBehaviour {

    Queue<KeyCode> simonHasSaid = new Queue<KeyCode>();     // This Queue is a backup for what Simon has said
    [SerializeField] float timeBetweenCommands = 1f;

    SimonManager simon;
    KeyCode[] keycodes;


    void Awake() {
        simon = RootManager.instance.simonManager;
        keycodes = simon.keycodes;
        simon.nextRoundCallback += NextRound;
    }


    void NextRound() { StartCoroutine(IENextRound()); }
    IEnumerator IENextRound() {
        KeyCode[] keys = simonHasSaid.ToArray();
        for (int i = 0; i < keys.Length; i++) {
            SayCommand(keys[i]);
            yield return new WaitForSeconds(timeBetweenCommands);
        }

        KeyCode nextCommand = keycodes[Random.Range(0, keycodes.Length - 1)];
        SayCommand(nextCommand);
        simonHasSaid.Enqueue(nextCommand);
        yield return new WaitForSeconds(timeBetweenCommands);

        simon.SetSimonsCommands(simonHasSaid);
        yield return null;
    }

    void SayCommand(KeyCode key) {
        simon.Highlight(key);
        // Debug.Log(key);
    }
}