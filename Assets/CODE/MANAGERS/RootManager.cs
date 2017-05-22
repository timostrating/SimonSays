using UnityEngine;

/// <summary>
/// This class is responsible for giving every class easy acces to all manager classes.
/// </summary>
public class RootManager : MonoBehaviour {

    #region SINGLETON PATTERN
    public static RootManager _instance;
    public static RootManager instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<RootManager>();

                if (_instance == null) {
                    GameObject container = new GameObject("RootManager");
                    _instance = container.AddComponent<RootManager>();
                }
            }
            return _instance;
        }
    }
    #endregion


    public CameraManager cameraManager;
    public SimonManager simonManager;
    public ScoreManager scoreManager;
}
