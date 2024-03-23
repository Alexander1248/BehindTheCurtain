using System;
using UnityEngine;

public class FenceAnimationRunner : MonoBehaviour
{
    public Transform left;
    public Transform right;

    private float _time = 0;

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        right.localEulerAngles = Vector3.forward * _time * 80;
        left.localEulerAngles = Vector3.back * (_time - 0.1f) * 80;
        
        if (_time > 1.1)
            Destroy(this);
    }
}