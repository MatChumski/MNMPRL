using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{

    [SerializeField] private List<GameObject> linkedEnemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(GameObject enemy in linkedEnemies)
            {
                enemy.GetComponent<EnemyChase>().active = true;
            }
        }
    }
}
