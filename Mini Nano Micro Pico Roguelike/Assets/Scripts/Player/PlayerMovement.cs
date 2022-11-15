using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] Vector2 movement;
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] Rigidbody2D rbPointer;

    [SerializeField] Animator animator;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement

        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.y = Input.GetAxisRaw("Vertical") * speed;
    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rbPlayer.MovePosition(rb.position);
        rbPointer.MovePosition(rb.position);

        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y)));
    }

}
