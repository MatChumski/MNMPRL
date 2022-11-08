using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] DamagePopup damagePopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player Weapon"))
        {
            Debug.Log("hit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Weapon"))
        {
            Weapon weapon = collision.GetComponent<Weapon>();

            if (weapon.attacking)
            {
                int damage = weapon.damage;
                damage *= Random.Range(1, 2);

                //Debug.Log("hit " + damage + " damage");
                damagePopup.Create(transform.position, damage);
            }
        }
    }
}
