using UnityEngine;

public class StatuePuzzle : MonoBehaviour
{
    public GameObject target;
    public GameObject[] statues;

    private void FixedUpdate()
    {
        bool solved = true;
        foreach (var t in statues)
        {
            Vector3 ideal = target.transform.position - t.transform.position;
            Vector3 dir = t.transform.forward;
            if (Vector3.Dot(ideal, dir) < 0.9)
                solved = false;
        }
        // On Solve
        Debug.unityLogger.Log(solved);
        
    }
}