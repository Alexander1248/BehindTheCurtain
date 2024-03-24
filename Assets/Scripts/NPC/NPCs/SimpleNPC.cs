 using UnityEngine;
 using UnityEngine.Events;

 public class SimpleNPC : NPC
 {
     [SerializeField] private UnityEvent[] onLine;
     [SerializeField] private UnityEvent onEnd;

     protected override void OnTextLine(int line)
     {
         if (line < onLine.Length)
            onLine[line].Invoke();
     }

     protected override void OnDialogEnd()
     {
         onEnd.Invoke();
     }
 }