using UnityEngine;

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

	void Start() {
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		currentRotation = transform.eulerAngles;
	}

	void LateUpdate() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) initialTouch = touch.position.y;
			if (touch.phase == TouchPhase.Moved && initialTouch > Screen.height/screenTouchRatio) {
				yaw += touch.deltaPosition.x * lookSensitivity *Time.deltaTime;
				pitch -= touch.deltaPosition.y * lookSensitivity/2 * Time.deltaTime;
				pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
			}
		}
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		
		//transform.eulerAngles = new Vector3(pitch, yaw);
		transform.eulerAngles = currentRotation;
		transform.position = target.position - offsetPos;
	}
}
