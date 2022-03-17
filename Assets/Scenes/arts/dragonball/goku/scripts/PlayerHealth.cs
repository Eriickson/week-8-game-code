using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    public float health;
    public float inmunityTime = 0.5f;
    bool isInmune;
    SpriteRenderer sprite;
    Blink material;
    public float knockbackForceX;
    public float knockbackForceY;
    Rigidbody2D rb;
    public Image healthImage;
    public GameObject gameOverImage;



    // Start is called before the first frame update
    void Start()
    {
        gameOverImage.SetActive(false);
        health = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = health / 100;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)


    {
        /* if (collision.transform.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
        } else
        {
            rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
        } */


        if (collision.CompareTag("Enemy") && !isInmune)
        {
           health -= collision.GetComponent<EnemyScript>().damageToGive;
            StartCoroutine(Inmune());
            if (health <= 0)
            {
                Time.timeScale = 0;
                gameOverImage.SetActive(true);
            }
        }
    }


    IEnumerator Inmune()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }
}
