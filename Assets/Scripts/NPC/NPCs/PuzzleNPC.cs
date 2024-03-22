using System;
using UnityEngine;

public class PuzzleNPC : NPC
{
    public StatueMovementController controller;
    public StatuePuzzle puzzle;
    [Space]
    public Controller player;

    protected override void OnTextLine(int line)
    {
        Vector3 delta;
        switch (line)
        {
            case 0:
                player.movementEnabled = false;
                player.lockCamera = true;
                player.lockReturnSpeed = 0.1f;
                
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
        // Если надо можно вызвать зачитывание текста, блокировку камеры и другое
    }

    protected override void OnDialogEnd()
    {
        player.movementEnabled = true;
        player.lockCamera = false;
        
        puzzle.enabled = true;
        controller.enabled = true;
    }
}