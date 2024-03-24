 using UnityEngine;

 public class ManaNPC : SlowmoNPC
 {
     public Controller controller;
     public SpellCaster caster;
     protected override void OnTextLine(int line)
     {
         base.OnTextLine(line);
         if (line != 0) return;
         controller.lockCamera = true;
         controller.lockReturnSpeed = 1;
         var fwd = controller.camera.transform.forward;
         controller.lockAngle = new Vector2(Mathf.Atan2(fwd.x, fwd.z), 
             Mathf.Atan2(fwd.y, new Vector2(fwd.x, fwd.z).magnitude)) * Mathf.Rad2Deg;
     }

     protected override void OnDialogEnd()
     {
         base.OnDialogEnd();
         controller.lockCamera = false;
         caster.SelectSpell(caster.disableIndex);
         caster.disableIndex = -1;
     }
 }