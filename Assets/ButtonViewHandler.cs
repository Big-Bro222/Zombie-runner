using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonViewHandler : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color hoverColor;
    [SerializeField] Color selectColor;

    private Vector3 originScale;
    void Start()
    {
        originScale = transform.localScale;
        transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultColor); ;

    }

    public void OnHoverEnter()
    {
        transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", hoverColor); ;


    }

    public void OnHoverExit()
    {
        transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultColor); ;
    }

    public void OnClickDown()
    {
        transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", selectColor); ;

    }

    public void OnClickUp()
    {
        transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", hoverColor); ;

    }

    //private void OnMouseEnter()
    //{
    //    OnHoverEnter();
    //}

    //private void OnMouseExit()
    //{
    //    OnHoverExit();
    //}
}
