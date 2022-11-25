using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] float speed;

    [SerializeField] public float distance;
    [SerializeField] private float chaseDistance;

    [SerializeField] private bool chase;

    [SerializeField] Animator animator;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        
        //Vector2 direction = player.transform.position - transform.position;
        if (distance < chaseDistance && active)
        {
            animator.SetFloat("Speed", Random.Range(1, 10));

            if (chase)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, Mathf.Abs(speed) * Time.deltaTime);
            }
            else
            {
                float addY = player.transform.position.y > transform.position.y ? -distance : distance;
                float addX = player.transform.position.x > transform.position.x ? -distance : distance;

                transform.position = Vector2.MoveTowards(
                    this.transform.position, 
                    new Vector3 (transform.position.x + addX, transform.position.y + addY, transform.position.z), 
                    Mathf.Abs(speed) * Time.deltaTime);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
        
    }
}
