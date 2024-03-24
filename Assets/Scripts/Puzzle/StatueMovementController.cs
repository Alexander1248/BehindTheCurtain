using UnityEngine;

public class StatueMovementController : MonoBehaviour
{
    [Range(0.1f, 9f)]
    public float sensetivity = 1;
    public Controller controller;
    public SpellCaster caster;
    [Space] 
    public GameObject mouseContip;
    
    private RaycastHit _hit;
    private GameObject _statue;
    private AudioSource _source;

    public bool IsOccupied { get; private set;  }
    
    private const string XAxis = "Mouse X";
    private void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out _hit, 5);
        if (_hit.collider != null && _hit.collider.gameObject.CompareTag("Statue"))
        {
            mouseContip.SetActive(true);
            caster.enabled = false;
            if (Input.GetMouseButtonDown(0))
            {
                controller.lockMouseActive = false;
                controller.LockMovement();
                _statue = _hit.collider.gameObject;
                _source = _statue.GetComponent<AudioSource>();
                if (_source) _source.Play();
                IsOccupied = true;
                return;
            }
        }
        else mouseContip.SetActive(false);

        if (Input.GetMouseButtonUp(0))
        {
            _statue = null;
            caster.enabled = true;
            controller.lockCamera = false;
            controller.UnlockMovement();
            if (_source) _source.Stop();
            IsOccupied = false;
            return;
        }

        if (!_statue || !Input.GetMouseButton(0)) return;
        Vector3 delta = (_statue.transform.position - transform.position).normalized;
        controller.lockCamera = true;
        controller.lockReturnSpeed = 0.1f;
        controller.lockAngle = new Vector2(Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg, 0);
        _statue.transform.Rotate(Vector3.up, -Input.GetAxis(XAxis) * sensetivity);
    }
    
    
}