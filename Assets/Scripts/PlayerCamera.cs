using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour {

	public bool lockCursor;
	public float lookSensitivity = 10;
	public Transform target;
	public Vector3 offsetPos;
	public float screenTouchRatio = 2.5f;
	float initialTouch;

	public float rotationSmoothTime = .12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;
	public Vector2 pitchMinMax = new Vector2(-40, 85);
	float yaw, pitch;

	GameObject sb;
	float initPosx;

	void Start() {
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		currentRotation = transform.eulerAngles;
		target = GameObject.FindGameObjectWithTag("Player").transform;
		sb = GameObject.Find("ScreenBlinder");
		initPosx = sb.transform.position.x;
		MoveBlinder(true);

		pitch = transform.eulerAngles.x;
		yaw = transform.eulerAngles.y;
	}

	void LateUpdate() {
		if (Input.touchCount > 0) {
			foreach (Touch t in Input.touches) {
				if (t.phase == TouchPhase.Moved && t.position.y > Screen.height / screenTouchRatio) {
					yaw += t.deltaPosition.x * lookSensitivity * Time.deltaTime;
					pitch -= t.deltaPosition.y * lookSensitivity / 2 * Time.deltaTime;
					pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
				}
			}
		}
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;
		transform.position = target.position - offsetPos;
		if (Input.GetKeyDown(KeyCode.Escape)) {
			MoveBlinder(false);
			Invoke("QuitLevel",1.5f);
		}
	}

	void QuitLevel() { SceneManager.LoadScene("MainMenu"); }

	public void MoveBlinder(bool isOpening) {
		if (isOpening) {
			LeanTween.moveX(sb, initPosx - 1200, 1.5f).setEaseInOutQuad();
		}
		else {
			LeanTween.moveX(sb, initPosx, 1.5f).setEaseInOutQuad();
		}
	}
}
