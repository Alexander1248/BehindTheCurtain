using UnityEngine;

public class PuzzleNPC : NPC
{
    public StatueMovementController controller;
    public StatuePuzzle puzzle;
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
                player.movementEnabled = false;
                player.lockCamera = true;
                player.lockReturnSpeed = 0.05f;
                
                delta = (transform.position - player.transform.position).normalized;
                player.lockAngle =  new Vector2(Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg, 0);
                break;
            case 1:
                
                delta = (puzzle.transform.position - player.transform.position).normalized;
                player.lockAngle =  new Vector2(Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg, 0);
                break;
            case 2:
                
                delta = (puzzle.target.transform.position - player.transform.position).normalized;
                player.lockAngle =  new Vector2(Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg, 0);
                break;
        }
    }

    protected override void OnDialogEnd()
    {
        player.movementEnabled = true;
        player.lockCamera = false;
        caster.enabled = true;
        
        puzzle.enabled = true;
        controller.enabled = true;
    }
}