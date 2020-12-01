using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTypeSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool defaultZombie = false;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject propertiesIndicator;
    private bool selected = false;

    private void Start()
    {
        if (defaultZombie)
        {
            GetComponent<Animator>().Play("Move");
        }
    }

    public void OnClicked()
    {
        selected = !selected;
        if (selected)
        {
            GlobalModel.Instance.ZombieTypes.Add(name);
            GetComponent<Outline>().OutlineWidth = 2;
            GetComponent<Animator>().Play("GetHit");
            audioSource.PlayOneShot(clickSFX);
        }
        else
        {
            GlobalModel.Instance.ZombieTypes.Remove(name);
            GetComponent<Outline>().OutlineWidth = 0;
            GetComponent<Animator>().Play("Idle");
            audioSource.PlayOneShot(clickSFX);


        }
    }

    public void OnHoverEnter()
    {
        propertiesIndicator.SetActive(true);
    }

    public void OnHoverExit()
    {
        propertiesIndicator.SetActive(false);
    }
}
