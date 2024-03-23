using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class StatuePuzzle : MonoBehaviour
{
    public GameObject target;
    public GameObject[] statues;
    public Vector3 statueForward = Vector3.back;
    [Range(0, 1)]
    public float accurancy = 0.9f;

    [Space] 
    public UnityEvent onSolved;

    private void FixedUpdate()
    {
        bool solved = true;
        foreach (var t in statues)
        {
            Vector3 ideal = (target.transform.position - t.transform.position).normalized;
            Vector3 dir = t.transform.rotation * statueForward;
            if (Vector3.Dot(ideal, dir) < accurancy)
                solved = false;
        }

        if (solved) onSolved.Invoke();
    }
}