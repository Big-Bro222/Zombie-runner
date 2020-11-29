using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponCarousel : MonoBehaviour
{
    [SerializeField] TextMeshPro weaponText;
    private void Start()
    {
        GetComponent<Carousel>().onItemChange += ItemChange;
    }

    private void ItemChange(int ItemIdex)
    {
        GetComponent<AudioSource>().Play();
        string name= transform.GetChild(ItemIdex).name;
        weaponText.text = name;
        GlobalModel.Instance.startWeapon = name;
    }
}
