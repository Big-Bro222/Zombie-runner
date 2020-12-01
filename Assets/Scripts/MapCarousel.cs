using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapCarousel : MonoBehaviour
{
    [SerializeField] TextMeshPro mapTitle;
    private void Start()
    {
        GetComponent<Carousel>().onItemChange += ItemChange;
    }

    private void ItemChange(int ItemIdex)
    {
        GetComponent<AudioSource>().Play();
        string name = transform.GetChild(ItemIdex).name;
        mapTitle.text = name;
        GlobalModel.Instance.map = name;
    }
}
