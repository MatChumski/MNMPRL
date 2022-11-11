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
    [SerializeField] private float attackTime;

    [SerializeField][Range(0,2)] private float attackSpeed;

    public int damage;
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackCooldown >= attackTime)
            {
                damage = Random.Range(1, 11);
                // Moves the weapon and rotates it according to the pointer
                transform.position = pointer.transform.position;
                transform.rotation = circularPointer.transform.rotation;

                // Plays the animation
                animator.speed = attackSpeed;
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
