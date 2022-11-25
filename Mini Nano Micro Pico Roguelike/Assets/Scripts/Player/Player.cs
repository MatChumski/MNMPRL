using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float health;

    public GameObject healthText;


    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("Health Text");

        Debug.Log(health);

    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.GetComponent<TextMeshProUGUI>().SetText(health.ToString());
        }
        
    }
}
