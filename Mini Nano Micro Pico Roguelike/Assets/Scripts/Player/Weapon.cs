using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject circularPointer;
    [SerializeField] private Animator animator;

    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject orb;

    public GameHandler gameHandler;

    public Player player;
    public float energyConsumption;

    public AudioSource weaponAudio;
    public AudioClip shootClip;
    public AudioClip magicClip;
    public AudioClip failClip;

    public BoxCollider2D boxCollider;

    public float attackTime;
    public string type;
    public string rarity;
    [Range(0, 2)] public float speed;
    public float damage;
    public string effect;

    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        pointer = GameObject.Find("Pointer");
        player = GameObject.Find("Player").GetComponent<Player>();
        circularPointer = GameObject.Find("Circular Pointer");

        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        //attackTime = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.gameStatus == "play")
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (attackCooldown >= attackTime)
                {
                    // Moves the weapon and rotates it according to the pointer
                    transform.position = pointer.transform.position;
                    transform.rotation = circularPointer.transform.rotation;

                    player.UpdateEnergy(player.energy - energyConsumption);

                    boxCollider.enabled = true;

                    if (type == "Wand" || type == "Bow")
                    {
                        weaponAudio.clip = failClip;

                        if (!player.isDebuff)
                        {
                            GameObject newProyectile;

                            if (type == "Wand")
                            {
                                newProyectile = Instantiate(orb);
                                weaponAudio.clip = magicClip;
                            }
                            else
                            {
                                newProyectile = Instantiate(arrow);
                                weaponAudio.clip = shootClip;
                            }

                            newProyectile.transform.position = pointer.transform.position;
                            newProyectile.transform.rotation = circularPointer.transform.rotation;
                            newProyectile.transform.Rotate(new Vector3(0, 0, -90));

                            newProyectile.GetComponent<Proyectile>().damage = damage;
                            newProyectile.GetComponent<Proyectile>().speed = speed;
                        }
                        weaponAudio.Play();

                        animator.speed = 1;
                    }
                    else
                    {
                        animator.speed = !player.isDebuff ? speed : speed * 0.65f;
                    }


                    // Plays the animation
                    animator.Play("Base Layer.Weapon_Attack");
                    attacking = true;
                    attackCooldown = !player.isDebuff ? 0 : -0.5f;


                }
            }
            else
            {
                if (attackCooldown < attackTime)
                {
                    attackCooldown += Time.deltaTime;
                }

                // If the animation time was completed, the weapon
                // returns to its original position
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    attacking = false;
                    boxCollider.enabled = false;
                    transform.SetPositionAndRotation(
                        transform.parent.gameObject.transform.position,
                        transform.rotation = new Quaternion(0, 0, 0, 0));
                }
            }
        }
    }
}
