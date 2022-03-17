using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    EnemyScript enemy;
    public GameObject deathEffect;
    private bool isDamaged;

    // Start is called before the first frame update
    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            enemy.healthPoints -= 2f;
            StartCoroutine(Damager());
            if(enemy.healthPoints <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
         }
    }


    IEnumerator Damager()
    {
        isDamaged = true;

        yield return new WaitForSeconds(0.5f);

        isDamaged = false;
    }

}
