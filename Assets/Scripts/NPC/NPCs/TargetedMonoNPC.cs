using UnityEngine;
using UnityEngine.Serialization;

public class TargetedMonoNPC : NPC
{
    public Transform target;
    [Space]
    public Controller player;

    protected override void OnTextLine(int line)
    {
        player.lockCamera = true;
        player.lockMouseActive = true;
        player.lockReturnSpeed = 0.05f;

        var delta = (target.transform.position - player.transform.position).normalized;
        player.lockAngle = new Vector2(Mathf.Atan2(delta.x, delta.z),
            Mathf.Atan2(delta.y, new Vector2(delta.x, delta.z).magnitude)) * Mathf.Rad2Deg;
    }

    protected override void OnDialogEnd()
    {
        player.lockCamera = false;
    }
}