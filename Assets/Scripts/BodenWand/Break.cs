using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public SpriteRenderer yourSpriteRenderer;
    public GameObject unbroken;
    //public float Torque;
    public GameObject broken;
    public PlayerController pc;
    private Rigidbody2D rb;

    
    
    void Start()
    {
        GameEvents.Respawn.AddListener(Respawn);
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FadeIn());
    }    
   
   

     IEnumerator FadeIn()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;
        rb.AddTorque(200);
        while (yourSpriteRenderer.color.a > 0)
        {
            alphaVal -= 0.2f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.05f); 
        }

        Destroy(gameObject);

        
    }


}
