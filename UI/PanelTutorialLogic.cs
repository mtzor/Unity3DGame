using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTutorialLogic : MonoBehaviour
{
    public GameObject[] images;
    int currentImage;
    // Start is called before the first frame update
    void Start()
    {
        currentImage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeImage(int i)
    {
        images[currentImage].SetActive(false);
        currentImage += i;
        Debug.Log("Image" + currentImage);
        if( currentImage<0 )
        {
            currentImage = images.Length + currentImage;
        }
        currentImage %= images.Length;
        
        images[currentImage].SetActive(true);
    }
}
