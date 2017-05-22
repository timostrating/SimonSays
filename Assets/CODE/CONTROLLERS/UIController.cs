using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] GameObject playUI;
    [SerializeField] GameObject playAgainUI;
    SimonManager simon;
    ScoreManager scoreManager;

    [SerializeField] Text highscoreField;
    [SerializeField] string highscoreString = "Highscore: ";
    [SerializeField] Text scoreField;
    [SerializeField] string scoreString = "Score: ";


    private void Awake() {
        simon = RootManager.instance.simonManager;
        simon.newGameCallback += NewGame;
        scoreManager = RootManager.instance.scoreManager;

        playAgainUI.SetActive(false);
        playUI.SetActive(true);
    }

    void NewGame() { StartCoroutine(IENewGame()); }
    IEnumerator IENewGame() {
        yield return new WaitForEndOfFrame();  // We run late, We must asure that everybody on the callback is called.
        playAgainUI.SetActive(true);
        highscoreField.text = highscoreString + scoreManager.highScore;
        scoreField.text = scoreString + scoreManager.oldScore;
        yield return null;
    }

}
