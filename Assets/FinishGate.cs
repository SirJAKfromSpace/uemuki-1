using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGate : MonoBehaviour
{
    public string sceneToLoad;
    public PlayerCamera cam;
    private void Start() {
        cam = GameObject.FindObjectOfType(typeof(PlayerCamera)) as PlayerCamera; ;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            cam.MoveBlinder(false);
            Invoke("NextLevel", 1.5f);
        }
    }
    void NextLevel() => SceneManager.LoadScene(sceneToLoad);
}
