using UnityEngine;

public class OnPlateChecker : MonoBehaviour
{
    public bool isOnPlate = false;
    ParticleSystem particles;
    public GateKeeping gatekeeper;
    Vector3 initPos;

    private void Start() {
        particles = GetComponent<ParticleSystem>();
        gatekeeper = GameObject.FindObjectOfType(typeof(GateKeeping)) as GateKeeping;
        initPos = transform.position;
    }

    private void Update() {
        if(transform.position.y <= -10) {
            transform.position = initPos;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("SokoPlate")) {
            isOnPlate = true;
            particles.Play();
            gatekeeper.CheckAllPlates();
        }

    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("SokoPlate")) {
            isOnPlate = false;
            particles.Stop();
            gatekeeper.CheckAllPlates();
        }
    }
}
