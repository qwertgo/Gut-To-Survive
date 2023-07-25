using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer yourSpriteRenderer;

    void Start()
    {
        StartCoroutine(Fading());
    }
     IEnumerator Fading()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;
        while (yourSpriteRenderer.color.a > 0)
        {
            alphaVal -= 0.1f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.05f); 
        }
    }

}
