using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Carousel : MonoBehaviour
{
    int currentIndex = 0;
    public bool isSelected=false;
    public event Action<int> onItemChange;

    private void Start()
    {
        SetItemActive();
    }

    // Update is called once per frame
    void Update()
    {
        int previousIndex = currentIndex;
        if (isSelected)
        {
                    ProcessScrollWheel();
        if (previousIndex != currentIndex)
        {
            SetItemActive();
        }
        }


    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentIndex >= transform.childCount - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentIndex <= 0)
            {
                currentIndex = transform.childCount - 1;
            }
            else
            {
                currentIndex--;
            }
        }
    }


    private void SetItemActive()
    {
        int itemIndex = 0;

        foreach (Transform item in transform)
        {
            if (itemIndex == currentIndex)
            {
                item.gameObject.SetActive(true);
                if (onItemChange != null)
                {
                    onItemChange(itemIndex);
                }
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            itemIndex++;
        }

    }
}
