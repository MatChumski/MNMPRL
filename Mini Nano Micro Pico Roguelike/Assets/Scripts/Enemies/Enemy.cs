using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;

    [SerializeField] DamagePopup damagePopup;

    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(100, 200);        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Hit(int damage)
    {
        int crit = Random.Range(1, 11);

        if (crit > 8)
        {
            damage *= 2;
        }

        //Debug.Log("hit " + damage + " damage");
        health -= damage;
        damagePopup.Create(transform.position, damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Weapon"))
        {
            Weapon weapon = collision.GetComponent<Weapon>();

            if (weapon.attacking)
            {
                int damage = (int)weapon.damage;
                Hit(damage);
            }
        }

        if (collision.CompareTag("PlayerProyectile"))
        {
            Proyectile proyectile = collision.GetComponent<Proyectile>();

            int damage = (int)proyectile.damage;
            Hit(damage);
            proyectile.resistance--;
        }
    }
}
