using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDown : MonoBehaviour
{
    private Animator anim;

    public static bool HubOpen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HubOpen == true)
        {
            StartCoroutine(OpenHudTimer());
        }
        else if (HubOpen == false)
        {
            StopAllCoroutines();
            anim.SetBool("HudOpen", false);
        }
    }

    IEnumerator OpenHudTimer()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("HudOpen", true);
    }
}
