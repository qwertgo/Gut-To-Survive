/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] PlayerController p;
    [SerializeField] TextMeshProUGUI deathSCoreText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            string deathScore = "Deaths \n" + p.DeathCount;


            deathSCoreText.text = deathScore;
            deathSCoreText.enabled = true;
            p.endedGame = true;
        }
    }
}
*/