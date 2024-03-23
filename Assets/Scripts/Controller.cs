using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    public float maxVelocityChange;
    public Camera camera;
    [Range(0.1f, 9f)]
    public float sensitivity = 1;
    [Range(0f, 90f)]
    public float yRotationLimit = 88f;

    public bool lockRotation;
    public Vector2 lockAngle = Vector2.zero;
    
    [Min(0.01f)]
    public float lockReturnSpeed = 0.1f;
    
    
    [Space]
    public float speed = 10;
    public float jumpSpeed = 10;
    public float dashSpeed = 30;
    public float dashCooldown = 2;

    
    [Space]
    public bool movementEnabled = true;
    public bool jumpEnabled;
    public bool dashEnabled;

    private Rigidbody _rigidbody;
    private bool _inJump;
    private int _dashCount;
    private float _dashTime;
    private Vector2 _rotation = Vector2.zero;
    private const string XAxis = "Mouse X";
    private const string YAxis = "Mouse Y";


    private bool isWalking;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footsteps;
    private bool soudReady;

    void Start(){
        jointOriginalPos = joint.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void camControl(){
        _rotation.x += Input.GetAxis(XAxis) * sensitivity;
        _rotation.y += Input.GetAxis(YAxis) * sensitivity;
        _rotation.y = Mathf.Clamp(_rotation.y, -yRotationLimit, yRotationLimit);

        if (lockRotation)
        {
            // TODO: May be exists better solution?
            if (Mathf.Abs(_rotation.x - lockAngle.x) > Mathf.Abs(_rotation.x + 360 - lockAngle.x))
                _rotation.x += 360;
            else if (Mathf.Abs(_rotation.x - lockAngle.x) > Mathf.Abs(_rotation.x - 360 - lockAngle.x))
                _rotation.x -= 360;
            
            if (Mathf.Abs(_rotation.y - lockAngle.y) > Mathf.Abs(_rotation.y + 360 - lockAngle.y))
                _rotation.y += 360;
            else if (Mathf.Abs(_rotation.y - lockAngle.y) > Mathf.Abs(_rotation.y - 360 - lockAngle.y))
                _rotation.y -= 360;

            _rotation = Vector2.Lerp(_rotation, lockAngle, lockReturnSpeed);
        }

        camera.transform.localRotation = Quaternion.AngleAxis(_rotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(_rotation.x, Vector3.up);
    }

    private void Update()
    {
        camControl();
        
        if (movementEnabled)
        {
            var dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                dir += transform.forward;
            if (Input.GetKey(KeyCode.S))
                dir -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                dir -= transform.right;
            if (Input.GetKey(KeyCode.D))
                dir += transform.right;

            if (dashEnabled)
            {
                if (_dashCount > 0)
                {
                    _dashTime += Time.deltaTime;
                    if (_dashTime > dashCooldown)
                    {
                        _dashCount--;
                        _dashTime = 0;
                    }
                }

                if (_dashCount < 3 && _inJump && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    _rigidbody.AddForce(dir * dashSpeed, ForceMode.VelocityChange);
                    _dashCount++;
                }
            }

            //transform.position += dir.normalized * (speed * Time.deltaTime);
        }
        
        if (!_inJump && jumpEnabled && Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            _inJump = true;
        }

        HeadBob();
    }

    public void Cutscene(Vector2 rot){
        lockRotation = true;
        lockAngle = rot;
        for(int i = 0; i < 100; i++)
            camControl();
        lockRotation = false;
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        movementEnabled = false;
        isWalking = false;
        jumpEnabled = false;
        dashEnabled = false;
    }

    public void endCS(Vector2 rot){
        lockRotation = true;
        lockAngle = rot;
        for(int i = 0; i < 100; i++)
            camControl();
        lockRotation = false;
        rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;
        movementEnabled = true;
        isWalking = false;
        jumpEnabled = false;
        dashEnabled = false;
    }

    void FixedUpdate(){
        if (!movementEnabled) return;

        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        targetVelocity = transform.TransformDirection(targetVelocity) * speed;

        if (targetVelocity != Vector3.zero) isWalking = true;
        else isWalking = false;

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void HeadBob()
    {
        if(isWalking)
        {
            timer += Time.deltaTime * bobSpeed;
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
            if (Mathf.Sin(timer) < 0.3f && soudReady){
                audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
                audioSource.Play();
                soudReady = false;
            }
            if (Mathf.Sin(timer) > 0.7f && !soudReady){
                soudReady = true;
            }
        }
        else
        {
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("JumpReset")) return;
        _inJump = false;
    }
}