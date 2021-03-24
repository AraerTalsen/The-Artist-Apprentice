using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Animate entity moving to signify it's making its move
public class CombatAnim : MonoBehaviour
{
    public Transform movePoint, retractPoint;

    //Move entity forward
    public void AnimTime()
    {
        LeanTween.move(gameObject, movePoint, 0.3f);
    }

    //Move entity back to original position
    public void Retract()
    {
        LeanTween.move(gameObject, retractPoint, 0.3f);
    }
}
