using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public float energy;
    public float maxEnergy;

    public GameObject healthText;
    public GameObject energyText;

    public GameHandler gameHandler;

    public AudioSource playerAudio;
    public AudioClip hurtClip;

    private float prevEnergy;

    public Weapon playerWeapon;

    public bool isDebuff;


    // Start is called before the first frame update
    void Start()
    {
        //healthText = GameObject.Find("Health Text");
        //energyText = GameObject.Find("Energy Text");
        playerWeapon = GameObject.FindGameObjectWithTag("Player Weapon").GetComponent<Weapon>();        
    }

    public float energyRegen = 1f;
    public float energyRegenTime = 1.5f;
    float regenTimer = 0f;

    float timeWithoutAttacking = 0f;

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.gameStatus == "play")
        {
            if (energy < 0 && prevEnergy < 0)
            {
                isDebuff = true;
            }
            else
            {
                isDebuff = false;
            }

            if (!playerWeapon.attacking)
            {
                timeWithoutAttacking += Time.deltaTime;
            }
            else
            {
                timeWithoutAttacking = 0;
            }

            if (regenTimer >= energyRegenTime)
            {
                float extraEnergy = Mathf.Pow(timeWithoutAttacking, 1.5f);
                extraEnergy = extraEnergy > 15 ? 15 : extraEnergy;

                UpdateEnergy(energy + (energyRegen + extraEnergy));

                regenTimer = 0f;
            }
            else
            {
                regenTimer += Time.deltaTime;
            }
        }
    }

    public void UpdateEnergy(float newEnergy)
    {
        prevEnergy = energy;
        energy = newEnergy > maxEnergy ? maxEnergy : newEnergy;
        energy = Mathf.RoundToInt(energy);
        energyText.GetComponent<TextMeshProUGUI>().SetText(energy.ToString());
    }

    public void UpdateHealth(float newHealth)
    {
        health = newHealth > maxHealth ? maxHealth : newHealth;
        healthText.GetComponent<TextMeshProUGUI>().SetText(health.ToString());

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeapon"))
        {
            Enemy enemy = collision.transform.parent.gameObject.GetComponent<Enemy>();

            if (enemy.attacking)
            {
                float dmg = enemy.damage;

                float newHealth = health - dmg;

                UpdateHealth(newHealth);

                playerAudio.clip = hurtClip;
                playerAudio.Play();
            }
        }

        if (collision.CompareTag("EnemyProyectile"))
        {
            Proyectile proyectile = collision.GetComponent<Proyectile>();

            float newHealth = health - proyectile.damage;

            UpdateHealth(newHealth);

            playerAudio.clip = hurtClip;
            playerAudio.Play();
        }
    }
}
