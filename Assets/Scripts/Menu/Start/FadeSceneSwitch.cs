using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class FadeSceneSwitch : MonoBehaviour
{
    public UnityEngine.U2D.SpriteShapeRenderer spriteRenderer;
    [SerializeField] List<UnityEngine.U2D.SpriteShapeRenderer> spriteRendereres = new List<UnityEngine.U2D.SpriteShapeRenderer>();
    public CinemachineVirtualCamera cam;
    public GameObject Player;
    public cameraViewFinder camfind;
    public HorizontalOffset hOffset;



    // Start is called before the first frame update
  IEnumerator Fade()
    {
     float alpha = 1;

        while(alpha >0)
        {
            alpha -= 0.5f * Time.deltaTime; ;
            foreach(var sprtRenderer in spriteRendereres)
            {
                Color col = sprtRenderer.color;
                col.a = alpha;
                sprtRenderer.color = col;
            }

            yield return null;
        }
        
    
    }
    	
    void Fade_()
    {
        StartCoroutine(Fade());
    }

    void PlayerActive()
    {
        Player.SetActive(true);
    }

    void Awake()
    {
        hOffset.enabled = false;
        camfind.enabled = true;
        Fade_();
        Invoke("PlayerActive",1);
        
    }
}
