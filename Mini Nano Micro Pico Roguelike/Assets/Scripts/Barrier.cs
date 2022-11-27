using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    public float health = 3;

    public AudioSource wallAudio;
    public AudioClip wallClip;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Hit()
    {
        health--;

        wallAudio.clip = wallClip;
        wallAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player Weapon") && collision.GetComponent<Weapon>().attacking))
        {
            Hit();
        }

        if (collision.CompareTag("PlayerProyectile"))
        {
            Hit();
            Destroy(collision.gameObject);
        }


    }
}
