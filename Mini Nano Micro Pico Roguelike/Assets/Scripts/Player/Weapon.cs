using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject circularPointer;
    [SerializeField] private Animator animator;

    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject orb;

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
        circularPointer = GameObject.Find("Circular Pointer");
        //attackTime = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (attackCooldown >= attackTime)
            {
                // Moves the weapon and rotates it according to the pointer
                transform.position = pointer.transform.position;
                transform.rotation = circularPointer.transform.rotation;



                if (type == "Wand" || type == "Bow")
                {
                    GameObject newProyectile;

                    if (type == "Wand")
                    {
                        newProyectile = Instantiate(orb);
                    } else
                    {
                        newProyectile = Instantiate(arrow);
                    }

                    newProyectile.transform.position = pointer.transform.position;
                    newProyectile.transform.rotation = circularPointer.transform.rotation;
                    newProyectile.transform.Rotate(new Vector3(0, 0, -90));

                    newProyectile.GetComponent<Proyectile>().damage = damage;
                    newProyectile.GetComponent<Proyectile>().speed = speed;

                    animator.speed = 1;
                } else
                {
                    animator.speed = speed;
                }

                

                // Plays the animation
                animator.Play("Base Layer.Weapon_Attack");
                attacking = true;
                attackCooldown = 0;


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
                transform.SetPositionAndRotation(
                    transform.parent.gameObject.transform.position,
                    transform.rotation = new Quaternion(0, 0, 0, 0));
            }
        }



    }
}
