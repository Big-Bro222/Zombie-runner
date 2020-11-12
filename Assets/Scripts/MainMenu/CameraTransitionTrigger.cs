using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTransitionTrigger : MonoBehaviour
{
    [SerializeField] AudioSource onhoverSFX;
    public UnityEvent OnClicked;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public bool isSelected=false;


    private void OnMouseDown()
    {
        OnClicked.Invoke();
        isSelected = true;
    }

    private void OnMouseEnter()
    {
        OnEnter.Invoke();
        if (!isSelected)
        {
            GetComponentInChildren<Outline>().OutlineWidth = 10;
            onhoverSFX.Play();
        }
    }

    private void OnMouseExit()
    {
        OnExit.Invoke();
        if (!isSelected)
        {
            ResetOutline();
            onhoverSFX.Play();
        }

    }

    public void ResetOutline()
    {
        GetComponentInChildren<Outline>().OutlineWidth = 0;
    }
}
