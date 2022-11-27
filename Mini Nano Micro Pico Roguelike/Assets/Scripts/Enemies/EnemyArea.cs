using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{

    public bool activated;

    [SerializeField] private List<GameObject> linkedEnemies;

    private void Start()
    {
        activated = false;
    }

    private void Update()
    {
        if (!activated)
        {
            foreach(GameObject enemy in linkedEnemies)
            {
                if (enemy.GetComponent<EnemyChase>().active)
                {
                    ActivateEnemies();
                }
            }
        }
    }

    public void ActivateEnemies()
    {
        foreach (GameObject enemy in linkedEnemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyChase>().active = true;
            }
        }

        activated = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateEnemies();
        }
    }
}
