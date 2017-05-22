using UnityEngine;


/// <summary>
/// 
/// </summary>
public class CameraManager : MonoBehaviour {

    public Camera mainCamera;


    void Awake() {
        mainCamera.targetTexture = null;
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(true);
    }
}
