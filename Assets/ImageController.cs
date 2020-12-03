using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    // Start is called before the first frame update
    float width;
    float height;
    public bool isOut;
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        height = GetComponent<RectTransform>().rect.height;
        isOut = true;
    }

    private void Update()
    {
        if (!isOut)
        {
            //to pull it back
            if(GetComponent<RectTransform>().anchoredPosition.y < height / 2)
            {
                GetComponent<RectTransform>().position += new Vector3(0, Time.deltaTime * 150, 0);
            }
        }
        else
        {
            if (GetComponent<RectTransform>().anchoredPosition.y > -height / 2)
            {
                GetComponent<RectTransform>().position -= new Vector3(0, Time.deltaTime * 150, 0);
            }
        }
    }
    public void Pull()
    {
        isOut = !isOut;
    }

}
