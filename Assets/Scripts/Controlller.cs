using System;
using UnityEditor.UIElements;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Camera camera;
    [Range(0.1f, 9f)]
    public float sensitivity = 1;
    [Range(0f, 90f)]
    public float yRotationLimit = 88f;

    public bool lockRotation;
    public Vector2 lockAngles = Vector2.zero;
    
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
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private Vector2 _rotation = Vector2.zero;
    private const string XAxis = "Mouse X";
    private const string YAxis = "Mouse Y";
    private void Update()
    {
        _rotation.x += Input.GetAxis(XAxis) * sensitivity;
        _rotation.y += Input.GetAxis(YAxis) * sensitivity;
        _rotation.y = Mathf.Clamp(_rotation.y, -yRotationLimit, yRotationLimit);
        if (lockRotation)
            _rotation = Vector2.Lerp(_rotation, lockAngles, lockReturnSpeed);
        
        camera.transform.localRotation = Quaternion.AngleAxis(_rotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(_rotation.x, Vector3.up);
        
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

            transform.position += dir.normalized * (speed * Time.deltaTime);
        }
        
        if (!_inJump && jumpEnabled && Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            _inJump = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("JumpReset")) return;
        _inJump = false;
    }
}