using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{

    public List<Sprite> spriteList = new List<Sprite>();
    public List<string> rarityList = new List<string>();
    public List<float> damageList = new List<float>();

    public string type;
    public string rarity;
    public float speed;
    public float damage;
    public string effect;
    public float attackTime;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //int randomRarity = Random.Range(0, spriteList.Count);

        //spriteRenderer.sprite = spriteList[randomRarity];
        //rarity = rarityList[randomRarity];
        //damage = damageList[randomRarity];
    }

    void SetRandomProperties()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
