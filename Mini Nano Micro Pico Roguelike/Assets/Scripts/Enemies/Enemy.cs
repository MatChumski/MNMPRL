using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;

    [SerializeField] private float damage;

    [SerializeField] DamagePopup damagePopup;

    [SerializeField] private GameObject weapon;

    private Animator weaponAnim;

    private EnemyChase chase;

    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        health += Random.Range(-10, 30);
        chase = gameObject.GetComponent<EnemyChase>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemyWeapon"))
            {
                weapon = child.gameObject;
            }
        }

        weaponAnim = weapon.GetComponent<Animator>();
    }

    float attackTime = 1.5f;
    float attackCooldown = 0;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (attackCooldown >= attackTime)
        {
            Attack();
            attackCooldown = 0;

            attacking = true;

            weaponAnim.speed = 1;
        }
        else
        {
            if (attackCooldown < attackTime)
            {
                attackCooldown += Time.deltaTime;
            }

            // If the animation time was completed, the weapon
            // returns to its original position
            if (weaponAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                attacking = false;
                weapon.transform.SetPositionAndRotation(
                    weapon.transform.parent.gameObject.transform.position,
                    weapon.transform.rotation = new Quaternion(0, 0, 0, 0));
            }
        }
    }

    void Hit(int dmg)
    {
        int crit = Random.Range(1, 11);

        if (crit > 8)
        {
            dmg *= 2;
        }

        //Debug.Log("hit " + damage + " damage");
        health -= dmg;
        damagePopup.Create(transform.position, dmg);
    }

    void Attack()
    {
        

        weaponAnim.Play("Base Layer.Weapon_Attack");
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

            chase.active = true;
        }

        if (collision.CompareTag("PlayerProyectile"))
        {
            Proyectile proyectile = collision.GetComponent<Proyectile>();

            int damage = (int)proyectile.damage;
            Hit(damage);
            proyectile.resistance--;

            chase.active = true;
        }
    }
}
