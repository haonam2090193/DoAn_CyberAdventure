using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSideway : MonoBehaviour
{
    [SerializeField] private float damage;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies" || collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
