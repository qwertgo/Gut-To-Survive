using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public PlayerController pc;
    public int revive =0;
    [SerializeField] AudioClip collectableClip;
    
        

    private void OnTriggerEnter2D(Collider2D collision)
    {   

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {  
            Destroy(gameObject);

            //AudioSource source = collision.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(collectableClip, transform.position);
            //source.PlayOneShot(collectableClip);
        }
    }
}


