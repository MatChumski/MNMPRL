using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{

    [SerializeField] float lifetime;
    [SerializeField] float timeAlive;

    public float speed;
    public float damage;
    public int resistance;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive < lifetime && resistance > 0)
        {
            timeAlive += Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }
    }
}
