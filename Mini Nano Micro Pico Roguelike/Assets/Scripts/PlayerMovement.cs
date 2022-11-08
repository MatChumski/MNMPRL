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

    // Start is called before the first frame update
    void Start()
    {
        
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
        rbPlayer.MovePosition(rbPlayer.position + movement * speed * Time.fixedDeltaTime);
        rbPointer.MovePosition(rbPlayer.position);

        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y)));
    }

}
