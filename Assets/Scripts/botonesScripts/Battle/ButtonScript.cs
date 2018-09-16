using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    
    private Button buton;

    // Use this for initialization
    void Start()
    {
        buton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setColor(Color color)
    {
        if (buton)
        {
            buton.image.color = color;
        }
    }
}
