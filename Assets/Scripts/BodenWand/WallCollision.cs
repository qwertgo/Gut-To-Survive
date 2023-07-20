using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WallCollision : MonoBehaviour
{
  public SpriteShapeRenderer yourSpriteRenderer;

 

 private void OnTriggerStay2D(Collider2D coll)
 {
    if(coll.gameObject.CompareTag("Player"))
    {

    StartCoroutine(FadeIn());
 }}

 private void OnTriggerExit2D(Collider2D collision)
 {
     if(collision.gameObject.CompareTag("Player"))
    {
    StartCoroutine(FadeOut());
    }
 }


    private IEnumerator FadeIn()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;

        while (yourSpriteRenderer.color.a >0)
        {
            alphaVal -= 0.1f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
    }

    private IEnumerator FadeOut()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;

        while (yourSpriteRenderer.color.a != 1)
        {
            alphaVal += 0.1f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
    }
}