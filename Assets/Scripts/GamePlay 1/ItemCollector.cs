using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public delegate void CollectCherry(int cherry); //Dinh nghia ham delegate 
    public static CollectCherry collectCherryDelegate; //Khai bao ham delegate
    private int moneys = 0;

    private void Start()
    {
        if (GameManager.HasInstance)
        {
            moneys = GameManager.Instance.Moneys;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_COLLECT);
            }
            Destroy(collision.gameObject);
            moneys++;
            GameManager.Instance.UpdateCherries(moneys);
            collectCherryDelegate(moneys); //Broadcast event
        }
    }
}
