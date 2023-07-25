using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WallCollision : MonoBehaviour
{
  public SpriteShapeRenderer yourSpriteRenderer;

 

 private void OnTriggerEnter2D(Collider2D coll)
 {
    if(coll.gameObject.CompareTag("Player"))
    {

    StartCoroutine(FadeIn());
    Debug.Log("Fade out");
 }}

 private void OnTriggerExit2D(Collider2D collision)
 {
     if(collision.gameObject.CompareTag("Player"))
    {
    StartCoroutine(FadeOut());
    Debug.Log("Fade in");
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