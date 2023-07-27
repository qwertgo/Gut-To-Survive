using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WallCollision : MonoBehaviour
{
    [SerializeField] float fadeSpeed = .2f;
    [SerializeField] List<SpriteShapeRenderer> spriteRendereres = new List<SpriteShapeRenderer>();

    [SerializeField] List<Lights> lights;

 

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());

        }
    }


    private IEnumerator FadeOut()
    {
        foreach(var light in lights)
        {
            light.TurnOn(fadeSpeed);
        }

        float alpha = 1;

        while(alpha > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime; ;
            foreach(var sprtRenderer in spriteRendereres)
            {
                Color col = sprtRenderer.color;
                col.a = alpha;
                sprtRenderer.color = col;
            }

            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        foreach(var light in lights)
        {
            light.TurnOff(fadeSpeed);
        }

        float alpha = 0;

        while (alpha < 1)
        {
            alpha += fadeSpeed * Time.deltaTime; ;
            foreach (var sprtRenderer in spriteRendereres)
            {
                Color col = sprtRenderer.color;
                col.a = alpha;
                sprtRenderer.color = col;
            }

            yield return null;
        }
    }
}