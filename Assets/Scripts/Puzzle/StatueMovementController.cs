using UnityEngine;

public class StatueMovementController : MonoBehaviour
{
    public Controller controller;
    public SpellCaster caster;
    
    private RaycastHit[] _hits = new RaycastHit[2];
    private GameObject _statue;
    
    
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
                    caster.cast = false;
                    controller.movementEnabled = true;
                    _statue = obj;
                    return;
                }
            }
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _statue = null;
            caster.cast = true;
            controller.lockRotation = false;
            controller.movementEnabled = true;
            return;
        }

        if (_statue && Input.GetMouseButton(0))
        {
            Vector3 delta = (_statue.transform.position - transform.position).normalized;
            controller.lockRotation = true;
            controller.lockAngles = new Vector2(
                Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg,
                0);
            
            
        }
    }
}