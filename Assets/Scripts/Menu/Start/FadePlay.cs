using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePlay : MonoBehaviour
{
    public UnityEngine.U2D.SpriteShapeRenderer spriteRenderer;
        [SerializeField] List<UnityEngine.U2D.SpriteShapeRenderer> spriteRendereres = new List<UnityEngine.U2D.SpriteShapeRenderer>();

    // Start is called before the first frame update
  IEnumerator FadeIn()
    {
            float alpha = 0;

        while(alpha <1)
        {
            alpha += 0.25f * Time.deltaTime; ;
            foreach(var sprtRenderer in spriteRendereres)
            {
                Color col = sprtRenderer.color;
                col.a = alpha;
                sprtRenderer.color = col;
            }

            yield return null;
        }
        
    
    }

    public void Fade()
    {
        StartCoroutine(FadeIn());
    }
}
