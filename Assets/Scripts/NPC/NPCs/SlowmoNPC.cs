 using UnityEngine;

 public class SlowmoNPC : NPC
 {
     public float timeScale; 
     protected override void OnTextLine(int line)
     {
         Time.timeScale = timeScale;
     }

     protected override void OnDialogEnd()
     {
         Time.timeScale = 1;
     }
 }