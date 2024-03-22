using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationHELPER : MonoBehaviour{
    public Boss boss;

    public void animationEvent(int type){
        boss.animationEvent(type);
    }
}
