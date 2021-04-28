using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISounds : MonoBehaviour, IPointerEnterHandler
{
    //Script to control all UI Sounds

    //Button Select Sounds (OnCLick)
    public void SelectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GameAudio.Instance.UI_Select);
    }

    //On Button Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot(GameAudio.Instance.UI_Hover);
    }

    public void RunButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GameAudio.Instance.Run);
    }

    public void HealSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GameAudio.Instance.Heal);
    }
}
