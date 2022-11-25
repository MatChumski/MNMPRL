using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    GameObject playerWeapon;

    [SerializeField] private bool isTouchingItem;
    [SerializeField] private GameObject touchingItem;
    [SerializeField] private GameObject pfItemPicker;

    [SerializeField] private ItemWeaponSpawner itemWeaponSpawner;


    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingItem && !playerWeapon.GetComponent<Weapon>().attacking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (touchingItem.tag)
                {
                    case "ItemWeapon":

                        WeaponItem weaponItem = touchingItem.GetComponent<WeaponItem>();
                        Weapon weapon = playerWeapon.GetComponent<Weapon>();

                        // Create a new replacement item before grabbing the previous one

                        if (weapon.type == "Stick")
                        {
                            Instantiate(weapon);
                            weapon.transform.position = weaponItem.transform.position;
                        }
                        else
                        {
                            itemWeaponSpawner.CloneItemWeapon(touchingItem.transform.position, weapon.type, weapon.rarity);
                        }
                        
                        // Replacing player's weapon properties with the item's

                        weapon.speed = weaponItem.speed;
                        weapon.rarity = weaponItem.rarity;
                        weapon.damage = weaponItem.damage;
                        weapon.effect = weaponItem.effect;
                        weapon.type = weaponItem.type;
                        weapon.attackTime = weaponItem.attackTime;

                        playerWeapon.GetComponent<SpriteRenderer>().sprite = touchingItem.GetComponent<SpriteRenderer>().sprite;

                        Destroy(touchingItem);

                        break;
                }
                
                Debug.Log("Picked: " + touchingItem.name);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("ItemWeapon")){

            isTouchingItem = true;
            touchingItem = collision.gameObject;
            GameObject itemPicker = Instantiate(pfItemPicker);
            itemPicker.transform.position = new Vector3(touchingItem.transform.position.x - 0.4f, touchingItem.transform.position.y + 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ItemWeapon"))
        {
            isTouchingItem = false;

            GameObject[] pickers = GameObject.FindGameObjectsWithTag("ItemPicker");

            for (int i = 0; i < pickers.Length; i++)
            {
                Destroy(pickers[i]);
            }
        }
    }
}
