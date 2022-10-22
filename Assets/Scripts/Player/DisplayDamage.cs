using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] float timeElapsed = 0.2f;
    [SerializeField] CanvasGroup damageCanvas;


    void Start()
    {
        damageCanvas.alpha = 0;
    }

    public void DamageDisplay()
    {
        damageCanvas.alpha = 1;
    }


    private void Update()
    {
        if(damageCanvas.alpha <= 1 && damageCanvas.alpha > 0)
        {
            damageCanvas.alpha -= timeElapsed * Time.deltaTime;
        }
    }
    //public void DamageDisplay()
    //{
    //    StartCoroutine(ShowSplatter());
    //}

    //IEnumerator ShowSplatter()
    //{
    //    damageCanvas.alpha = 1;
    //    yield return new WaitForSeconds(timeElapsed);
    //    while(damageCanvas.alpha > 0)
    //    {
    //    damageCanvas.alpha -= timeElapsed * Time.deltaTime;
    //    }
    //}

}
