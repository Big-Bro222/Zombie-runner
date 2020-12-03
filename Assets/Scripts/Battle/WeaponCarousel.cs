using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponCarousel : MonoBehaviour
{
    [SerializeField] TextMeshPro weaponText;
    private List<GameObject> weaponPrefabs;

    private void Start()
    {
        weaponPrefabs = new List<GameObject>();
        GetComponent<Carousel>().onItemChange += ItemChange;
        foreach(Transform weapon in transform)
        {
            weaponPrefabs.Add(weapon.gameObject);
        }
    }

    private void ItemChange(int ItemIdex)
    {
        GetComponent<AudioSource>().Play();
        string name= transform.GetChild(ItemIdex).name;
        weaponText.text = name;
        GlobalModel.Instance.startWeapon = name;
    }

    public void SetWeapon(string name)
    {
        foreach(GameObject weaponPrefab in weaponPrefabs)
        {
            if (weaponPrefab.name.Equals(name))
            {
                weaponPrefab.SetActive(true);
            }
            else
            {
                weaponPrefab.SetActive(false);
            }
        }
        weaponText.text = name;
    }
}
