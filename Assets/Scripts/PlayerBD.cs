using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBD : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        anime = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Dead();
        }
    }
    private void Dead()
    {
        rb.bodyType = RigidbodyType2D.Static;
        rb.velocity = new Vector2(0, 0);
        anime.SetTrigger("Death");
    }

}
