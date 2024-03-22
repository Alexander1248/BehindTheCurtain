using System;
using UnityEngine;

public class PuzzleNPC : NPC
{
    public StatueMovementController controller;
    public StatuePuzzle puzzle;

    protected override void OnTextLine(int line)
    {
        Debug.Log(line);
        // Если надо можно вызвать зачитывание текста, блокировку камеры и другое
    }

    protected override void OnDialogEnd()
    {
        puzzle.enabled = true;
        controller.enabled = true;
    }
}