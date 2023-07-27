using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFade : MonoBehaviour
{
    public SpriteRenderer yourSpriteRenderer;
    public GameObject Highscore;

    public void Fade()
    {
        StartCoroutine(FadeIn());
    }
    
   private IEnumerator FadeIn()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;

        while (yourSpriteRenderer.color.a != 0.5f)
        {
            alphaVal += 0.1f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.02f); // update interval
        }

        if(yourSpriteRenderer.color.a ==0.5f)
        {
               Highscore.SetActive(true);    
        }
    }

}
