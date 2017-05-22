using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is responsible for all things the Simon AI would do.
/// </summary>
public class SimonController : MonoBehaviour {

    Queue<KeyCode> simonHasSaid = new Queue<KeyCode>();     // This Queue is a backup for what Simon has said
    [SerializeField] float timeBetweenCommands = 1f;

    SimonManager simon;
    KeyCode[] keycodes;


    void Awake() {
        simon = RootManager.instance.simonManager;
        keycodes = simon.keycodes;
        simon.nextRoundCallback += NextRound;
        simon.newGameCallback += NewGame;
    }


    void NextRound() { StartCoroutine(IENextRound()); }
    IEnumerator IENextRound() {
        if (simon.gameState == GameState.Paused)
            yield return null;

        KeyCode[] keys = simonHasSaid.ToArray();
        for (int i = 0; i < keys.Length; i++) {
            yield return new WaitForSeconds(timeBetweenCommands);
            SayCommand(keys[i]);
        }

        KeyCode nextCommand = keycodes[Random.Range(0, keycodes.Length - 1)];
        yield return new WaitForSeconds(timeBetweenCommands);
        SayCommand(nextCommand);
        simonHasSaid.Enqueue(nextCommand);

        simon.SetSimonsCommands(simonHasSaid);
        yield return null;
    }

    void SayCommand(KeyCode key) {
        simon.HighlightButton(key);
        // Debug.Log(key);
    }

    void NewGame() {
        simonHasSaid = new Queue<KeyCode>();
    }
}