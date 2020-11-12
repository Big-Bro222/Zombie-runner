using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayController : MonoBehaviour
{
    Image image;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip AudioClip;

    private void Start()
    {
        image = GetComponent<Image>();
    }


    public void SetOnHover(bool onHover)
    {
        if (onHover)
        {
            audioSource.PlayOneShot(AudioClip);
        }
        else
        {
            StopAllCoroutines();
            image.color= new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }

   


}
