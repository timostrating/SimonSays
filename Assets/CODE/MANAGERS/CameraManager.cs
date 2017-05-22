using UnityEngine;


public class CameraManager : MonoBehaviour {

    public Camera mainCamera;
    public Camera screenCamera;

    [SerializeField] RenderTexture screenTexture;


    void Awake() {
        mainCamera.targetTexture = null;
        mainCamera.gameObject.SetActive(true);
        screenCamera.gameObject.SetActive(false);
    }
}
