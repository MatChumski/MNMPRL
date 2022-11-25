using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public string type;
    public int health;

    public ItemWeaponSpawner weaponSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
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

                float spawnOffsetX = Random.Range(-2.5f, 2.5f);
                float spawnOffsetY = Random.Range(-2.5f, 2.5f);

                weaponSpawner.CreateRandomWeapon(spawnPos, spawnOffsetX, spawnOffsetY);

                break;
        }

        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player Weapon") && collision.GetComponent<Weapon>().attacking) || collision.CompareTag("PlayerProyectile"))
        {
            Drop();
        }
    }
}
