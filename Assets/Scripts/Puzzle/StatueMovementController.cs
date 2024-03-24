using UnityEngine;

public class StatueMovementController : MonoBehaviour
{
    [Range(0.1f, 9f)]
    public float sensetivity = 1;
    public Controller controller;
    public SpellCaster caster;
    [Space] 
    
    private RaycastHit[] _hits = new RaycastHit[2];
    private GameObject _statue;
    private AudioSource _source;
    
    
    private const string XAxis = "Mouse X";
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var t = transform;
            var size = Physics.RaycastNonAlloc(t.position, t.forward, _hits, 3);
            for (int i = 0; i < size; i++)
            {
                GameObject obj = _hits[i].collider.gameObject;
                if (obj.CompareTag("Statue"))
                {
                    caster.enabled = false;
                    controller.lockMouseActive = false;
                    controller.LockMovement();
                    _statue = obj;
                    _source = _statue.GetComponent<AudioSource>();
                    if (_source) _source.Play();
                    return;
                }
            }
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _statue = null;
            caster.enabled = true;
            controller.lockCamera = false;
            controller.UnlockMovement();
            if (_source) _source.Stop();
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