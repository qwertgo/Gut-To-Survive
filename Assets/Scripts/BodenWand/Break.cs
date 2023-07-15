using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public SpriteRenderer yourSpriteRenderer;
    public GameObject unbroken;
    //public float Torque;
    public GameObject broken;
    
    public Rigidbody2D rb;
    
 
    bool GroundCheck()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, 0.86f, ~LayerMask.GetMask("Player")))
        {
            return true;
           
        }

        else
        {
            return false;
        }
    }
    
    void Start()
    {
        Fade();
    }

     IEnumerator FadeIn()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;
       
        while (yourSpriteRenderer.color.a > 0)
        {
            alphaVal -= 0.2f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.05f); 
            
          
        }
      if(yourSpriteRenderer.color.a <=0.05f)
      {
        Destroy(gameObject);
      }
        
    }
    
    public void Fade()
    {
        StartCoroutine(FadeIn());
    }
}
