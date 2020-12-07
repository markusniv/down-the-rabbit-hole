using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantSceneScript : MonoBehaviour
{
    Text priceOne, priceTwo, priceThree;

    // Start is called before the first frame update
    void Start()
    {
        priceOne = GameObject.Find("PriceOne").GetComponent<Text>();
        priceTwo = GameObject.Find("PriceTwo").GetComponent<Text>();
        priceThree = GameObject.Find("PriceThree").GetComponent<Text>();

        var possibleConsumables = Resources.LoadAll<GameObject>("Prefabs/Items/Consumables");
        //var possibleRelics = Resources.LoadAll<GameObject>("Prefabs/Items/Relics");
        var possibleWeapons = Resources.LoadAll<GameObject>("Prefabs/Items/Weapons");
        var selectedItemOne = possibleConsumables[Random.Range(0, possibleConsumables.Length)];
        //var selectedItemTwo = possibleRelics[Random.Range(0, possibleRelics.Length)];
        var selectedItemThree = possibleWeapons[Random.Range(0, possibleWeapons.Length)];

        var itemOne = Instantiate(selectedItemOne, new Vector3(-7, -2, 0), Quaternion.identity);
        //var itemTwo = Instantiate(selectedItemTwo, new Vector3(0, -2, 0), Quaternion.identity);
        var itemThree = Instantiate(selectedItemThree, new Vector3(7, -2, 0), Quaternion.identity);

        itemOne.name = itemOne.name.Replace("(Clone)", "");
        //itemTwo.name = itemTwo.name.Replace("(Clone)", "");
        itemThree.name = itemThree.name.Replace("(Clone)", "");

        priceOne.text = itemOne.GetComponent<Item>().Value.ToString();
        //priceTwo.text = itemTwo.GetComponent<Item>().Value.ToString();
        priceThree.text = itemThree.GetComponent<Item>().Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
