using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public string type;
    public int health;

    private bool alive;

    public ItemWeaponSpawner weaponSpawner;

    public AudioSource chestAudio;
    public AudioClip chestClip;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Drop()
    {
        switch (type)
        {
            case "weapon":

                Vector3 spawnPos = gameObject.transform.position;

                Debug.Log("spawnpos1: " + spawnPos);

                float spawnOffsetX = Random.Range(-0.1f, 0.1f);
                float spawnOffsetY = Random.Range(-0.1f, 0.1f);

                weaponSpawner.CreateRandomWeapon(spawnPos, spawnOffsetX, spawnOffsetY);

                break;
        }

        health--;
        if (health <= 0)
        {
            alive = false;

            transform.position = new Vector3(50, 50);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alive)
        {
            if ((collision.CompareTag("Player Weapon") && collision.GetComponent<Weapon>().attacking) || collision.CompareTag("PlayerProyectile"))
            {
                chestAudio.clip = chestClip;
                chestAudio.Play();
                Drop();
            }
        }
    }
}
