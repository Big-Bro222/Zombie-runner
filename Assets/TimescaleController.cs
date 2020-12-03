using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimescaleController : MonoBehaviour
{
    private bool isScalable;
    [SerializeField] float scaleLoadingTime;
    [SerializeField] Image timeImage;
    // Start is called before the first frame update
    void Start()
    {
        isScalable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isScalable)
            {
                timeImage.fillAmount = 1;
                Time.timeScale = 0.3f;
                Invoke("Resume",5);
                isScalable = false;
            }
        }

        if (!isScalable)
        {
            if(timeImage.fillAmount > 0)
            {
                timeImage.fillAmount -= Time.deltaTime*0.2f;
            }
            else
            {
                isScalable = true;
            }
        }
    }


    private void Resume()
    {
        Time.timeScale = 1;
    }



}
