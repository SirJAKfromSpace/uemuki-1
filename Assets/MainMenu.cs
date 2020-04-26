using UnityEngine;

public class MainMenu : MonoBehaviour {
    public float rotateSpeed = 1;

    void Update() {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
    }
}
