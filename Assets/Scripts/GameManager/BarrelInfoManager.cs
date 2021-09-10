using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelInfoManager : MonoBehaviour
{
    public QuestItemProfile barrel;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GlobalControl.Instance.onBarrelPickupCallBack != null)
            {
                GlobalControl.Instance.onBarrelPickupCallBack.Invoke(barrel);
            }

            this.gameObject.SetActive(false);
        }

    }
}
