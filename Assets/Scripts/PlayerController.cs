using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    Rigidbody rigid;
    Vector3 inputDir;
    public float moveSpeed, jumpForce;

    public float smoothTurnTime = 0.2f;
    float smoothTurnVel;

    public float dist2Ground = 0.5f, dist2Push;
    public bool isGrounded, isPushing;
    public bool isPressing;
    int collMaskGrnd, collMaskPush;
    
    Animator anim;
    public Joystick joystick;
    public Transform cameraTransform;

    public Vector3 levelStartPosition;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody>();
        rigid.centerOfMass = Vector3.zero;
        rigid.inertiaTensorRotation = new Quaternion(0, 0, 0, 1);
        Bounds origin = transform.GetChild(2).GetComponent<BoxCollider>().bounds;
        dist2Push = origin.extents.z;
        
        joystick = GameObject.Find("Floating Joystick").GetComponent<Joystick>();
        collMaskGrnd = LayerMask.GetMask("Ground");
        collMaskPush = LayerMask.GetMask("Pushable");
        
        anim = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        levelStartPosition = transform.position;
    }

    void Update() {
        if (inputDir != Vector3.zero) {
            isPushing = Physics.Raycast(transform.position + Vector3.up, transform.forward, dist2Push + 0.2f, collMaskPush);
            anim.SetBool("isPushing", isPushing);
            Rotate();
        }
        else
            anim.SetBool("isPushing", false);

        if (transform.position.y < -10) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        inputDir = Vector3.ClampMagnitude(new Vector3(joystick.Horizontal, 0, joystick.Vertical), 1);
        anim.SetFloat("speedPercent", inputDir.magnitude);
        rigid.MovePosition(transform.position + transform.forward * moveSpeed * inputDir.magnitude * Time.deltaTime);

        //isPressing = Input.GetKeyDown(KeyCode.Space);
        isGrounded = Physics.Raycast(transform.position + transform.up*0.1f, -transform.up, dist2Ground, collMaskGrnd | collMaskPush);
        if (!isGrounded)
            anim.SetBool("isFalling", true);
        else
            anim.SetBool("isFalling", false);

        //if (isGrounded && isPressing) {
        //    anim.SetBool("isJumping", true);
        //    rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}
        //else if (!isGrounded) anim.SetBool("isJumping", false);
        //anim.SetBool("isGrounded", isGrounded);
        DebugStuff();
    }

    void Rotate() {
        float targetRot = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        Vector3 angle = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref smoothTurnVel, smoothTurnTime);
        rigid.rotation = Quaternion.Euler(angle);
    }

    void DebugStuff() {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * dist2Push, Color.red);
        Debug.DrawRay(transform.position + transform.up*0.1f, -transform.up * dist2Ground, Color.red);
    }
}