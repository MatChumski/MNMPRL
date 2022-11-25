using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeaponSpawner : MonoBehaviour
{

    [SerializeField] GameObject sword;
    [SerializeField] GameObject dagger;
    [SerializeField] GameObject greataxe;
    [SerializeField] GameObject handaxe;
    [SerializeField] GameObject mace;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject wand;

    private void SetProperties(WeaponItem weapon, int rarityPos)
    {
        weapon.damage = weapon.damageList[rarityPos];
        weapon.rarity = weapon.rarityList[rarityPos];
        weapon.gameObject.GetComponent<SpriteRenderer>().sprite = weapon.spriteList[rarityPos];

    }

    public void CreateRandomWeapon(Vector3 spawnPos, float spawnOffsetX, float spawnOffsetY)
    {
        List<GameObject> itemList = new List<GameObject>()
        {
            sword,
            dagger,
            greataxe,
            handaxe,
            mace,
            bow,
            wand
        };

        GameObject item = Instantiate(itemList[Random.Range(0, itemList.Count)]);

        WeaponItem newWeapon = item.GetComponent<WeaponItem>();

        int rarity = Random.Range(0, newWeapon.rarityList.Count);

        newWeapon.rarity = newWeapon.rarityList[rarity];
        newWeapon.damage = newWeapon.damageList[rarity];
        newWeapon.GetComponent<SpriteRenderer>().sprite = newWeapon.spriteList[rarity];

        item.transform.position = new Vector3(spawnPos.x + spawnOffsetX, spawnPos.y + spawnOffsetY);

    }

    public void CloneItemWeapon(Vector3 position, string type, string rarity)
    {
        GameObject spawning;
        type = type.ToLower();

        switch (type)
        {
            case "sword":

                spawning = Instantiate(sword);
                break;

            case "dagger":

                spawning = Instantiate(dagger);
                break;

            case "greataxe":

                spawning = Instantiate(greataxe);
                break;

            case "handaxe":

                spawning = Instantiate(handaxe);
                break;

            case "mace":

                spawning = Instantiate(mace);
                break;

            case "bow":

                spawning = Instantiate(bow);
                break;

            case "wand":

                spawning = Instantiate(wand);
                break;

            default:

                spawning = Instantiate(sword);
                break;
        }

        WeaponItem newItem = spawning.GetComponent<WeaponItem>();

        int rarityPos = newItem.rarityList.IndexOf(rarity);

        if (rarityPos < 0)
        {
            rarityPos = 0;
        }

        SetProperties(newItem, rarityPos);

        spawning.transform.position = position;
    }
}
