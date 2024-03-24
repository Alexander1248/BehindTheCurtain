 using System;
 using UnityEngine;

 public class SlowmoNPC : NPC
 {
     public float timeScale;
     
     [Space]
     public Material grayscaleMaterial;
     public float grayscaleValue = 0.6f;
     private float _buffer;
     private float _step;
     
     private bool _toGray;
     private bool _fromGray;
     
     protected override void OnTextLine(int line)
     {
         if (line != 0) return;
         Time.timeScale = timeScale;

         if (grayscaleMaterial)
         {
             _buffer = grayscaleMaterial.GetFloat("_Grayscale");
             _toGray = true;
         }
     }

     protected override void OnDialogEnd()
     {
         Time.timeScale = 1;
         if (grayscaleMaterial)
         {
             _fromGray = true;
         }
     }

     protected override void Update()
     {
         try
         {
             base.Update();
         }
         catch (Exception e)
         {
             Debug.LogError(e);
         }

         if (grayscaleMaterial == null) return;
         
         if (_toGray)
         {
             var delta = (grayscaleValue - _buffer) * Time.unscaledDeltaTime;
             var val = grayscaleMaterial.GetFloat("_Grayscale") + delta;
             
             if (Mathf.Abs(val - grayscaleValue) <= Mathf.Abs(delta))
             {
                 grayscaleMaterial.SetFloat("_Grayscale", grayscaleValue);
                 _toGray = false;
             }
             else grayscaleMaterial.SetFloat("_Grayscale", val);
         }
         
         if (_fromGray)
         {
             var delta = (_buffer - grayscaleValue) * Time.unscaledDeltaTime;
             var val = grayscaleMaterial.GetFloat("_Grayscale") + delta;
             
             if (Mathf.Abs(val - _buffer) <= Mathf.Abs(delta)) 
             {
                 grayscaleMaterial.SetFloat("_Grayscale", _buffer);
                 _fromGray = false;
             }
             else grayscaleMaterial.SetFloat("_Grayscale", val);
         }
     }
 }