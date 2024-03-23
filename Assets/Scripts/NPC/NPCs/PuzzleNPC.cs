using UnityEngine;

public class PuzzleNPC : NPC
{
    public StatueMovementController controller;
    public StatuePuzzle puzzle;
    public Transform npc;
    [Space]
    public Controller player;

    public SpellCaster caster;

    private void Start()
    {
        foreach (var t in puzzle.statues) 
            t.transform.Rotate(Vector3.up, Random.Range(-180, 180));
    }

    protected override void OnTextLine(int line)
    {
        Vector3 delta;
        switch (line)
        {
            case 0:
                caster.enabled = false;
                player.LockMovement();
                player.lockCamera = true;
                player.lockMouseActive = true;
                player.lockReturnSpeed = 0.05f;
                
                delta = (npc.transform.position - player.transform.position).normalized;
                player.lockAngle =  new Vector2(Mathf.Atan2(delta.x, delta.z), 
                    Mathf.Atan2(delta.y, new Vector2(delta.x, delta.z).magnitude)) * Mathf.Rad2Deg;
                break;
            case 1:
                delta = (puzzle.transform.position - player.transform.position).normalized;
                player.lockAngle =  new Vector2(Mathf.Atan2(delta.x, delta.z), 
                    Mathf.Atan2(delta.y, new Vector2(delta.x, delta.z).magnitude)) * Mathf.Rad2Deg;
                break;
            case 2:
                delta = (puzzle.target.transform.position - player.transform.position).normalized;
                player.lockAngle =  new Vector2(Mathf.Atan2(delta.x, delta.z), 
                    Mathf.Atan2(delta.y, new Vector2(delta.x, delta.z).magnitude)) * Mathf.Rad2Deg;
                break;
        }
    }

    protected override void OnDialogEnd()
    {
        player.UnlockMovement();
        player.lockCamera = false;
        caster.enabled = true;
        
        puzzle.enabled = true;
        controller.enabled = true;
    }
}