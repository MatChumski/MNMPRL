using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    public float health;
    public float maxHealth;

    public float damage;
    public DamagePopup damagePopup;

    public GameObject weapon;
    public float attackSpeed;
    public float attackDistance;
    public GameObject arrow;
    public Animator weaponAnim;
    public bool attacking;

    public BoxCollider2D weaponBoxCollider;
    public CircleCollider2D weaponCircleCollider;

    public GameObject enemyHealth;

    public EnemyChase chase;

    public Player player;
    public Weapon playerWeapon;

    public AudioSource enemyAudio;
    public List<AudioClip> enemyDmgClips;
    public AudioClip rangeAttackClip;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth += Random.Range(-10, 30);
        health = maxHealth;
        chase = gameObject.GetComponent<EnemyChase>();

        player = GameObject.Find("Player").GetComponent<Player>();
        playerWeapon = GameObject.FindGameObjectWithTag("Player Weapon").GetComponent<Weapon>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemyWeapon"))
            {
                weapon = child.gameObject;
            }
            if (child.CompareTag("EnemyHealth"))
            {
                enemyHealth = child.gameObject;
            }
        }

        enemyHealth.GetComponent<TextMeshPro>().SetText(health + "/" + maxHealth);

        weaponBoxCollider = weapon.GetComponent<BoxCollider2D>();
        weaponCircleCollider = weapon.GetComponent<CircleCollider2D>();

        weaponAnim = weapon.GetComponent<Animator>();


    }

    public float rangeAttackTime = 1.5f;
    public float rangeAttackCooldown = 0;
    public float attackTime = 1.5f;
    public float attackCooldown = 0;


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            player.UpdateEnergy(player.energy + 30);
            player.UpdateHealth(player.health + Mathf.CeilToInt(maxHealth * 0.10f));
            Destroy(gameObject);
        }

        if (chase.active && chase.distance < attackDistance)
        {
            if (attackCooldown >= attackTime)
            {
                Attack();
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
                    if (weaponBoxCollider != null)
                    {
                        weaponBoxCollider.enabled = false;
                    }
                    if (weaponCircleCollider != null)
                    {
                        weaponCircleCollider.enabled = false;
                    }

                    attacking = false;

                    weapon.transform.SetPositionAndRotation(
                        weapon.transform.parent.gameObject.transform.position,
                        weapon.transform.rotation = new Quaternion(0, 0, 0, 0));
                }
            }

            if (arrow != null)
            {
                if (rangeAttackCooldown >= rangeAttackTime)
                {
                    Shoot();
                }
                else
                {
                    if (rangeAttackCooldown < rangeAttackTime)
                    {
                        rangeAttackCooldown += Time.deltaTime;
                    }
                }
            }
        }

    }

    public void Hit(int dmg)
    {
        enemyAudio.clip = enemyDmgClips[Random.Range(0, enemyDmgClips.Count)];
        enemyAudio.Play();

        int crit = Random.Range(1, 100);

        if (crit > 95)
        {
            dmg *= 2;
        }

        //Debug.Log("hit " + damage + " damage");
        health -= dmg;
        damagePopup.Create(transform.position, dmg);
        enemyHealth.GetComponent<TextMeshPro>().SetText(health + "/" + maxHealth);

    }

    public void Attack()
    {
        weaponAnim.Play("Base Layer.Weapon_Attack");
        attackCooldown = Random.Range(-0.5f, 0.5f);

        attacking = true;

        if (weaponBoxCollider != null)
        {
            weaponBoxCollider.enabled = true;
        }
        if (weaponCircleCollider != null)
        {
            weaponCircleCollider.enabled = true;
        }

        weaponAnim.speed = attackSpeed;
    }

    public void Shoot()
    {
        if (player != null)
        {
            GameObject newArrow = Instantiate(arrow);

            newArrow.transform.position = transform.position;

            GameObject goPlayer = player.gameObject;

            Quaternion rotation = Quaternion.LookRotation(goPlayer.transform.position - newArrow.transform.position, newArrow.transform.TransformDirection(Vector3.up));
            newArrow.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            float correction = (goPlayer.transform.position.x > newArrow.transform.position.x ? -90f : 90f);

            newArrow.transform.Rotate(0, 0, correction);

            rangeAttackCooldown = 0;

            enemyAudio.clip = rangeAttackClip;
            enemyAudio.Play();
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Weapon"))
        {
            if (playerWeapon.attacking || ((playerWeapon.type == "Bow" || playerWeapon.type == "Wand") && player.isDebuff))
            {
                int damage = !player.isDebuff ? (int)playerWeapon.damage : Mathf.CeilToInt(playerWeapon.damage * 0.5f);
                Hit(damage);
            }

            chase.active = true;
        }

        if (collision.CompareTag("PlayerProyectile"))
        {
            Proyectile proyectile = collision.GetComponent<Proyectile>();

            int damage = !player.isDebuff ? (int)proyectile.damage : Mathf.CeilToInt(proyectile.damage * 0.5f);
            Hit(damage);
            proyectile.resistance--;

            chase.active = true;
        }
    }
}
