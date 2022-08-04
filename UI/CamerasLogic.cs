using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasLogic : MonoBehaviour
{
    public GameObject[] Cameras;
    // Start is called before the first frame update
    public int activeIndex;
    void Start()
    {
        activeIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCameras()
    {
        Cameras[activeIndex].SetActive(false);
        Debug.Log("Deactivated" + activeIndex);
        activeIndex += 1;
        activeIndex %= Cameras.Length;
        Debug.Log("camera index" + activeIndex);
        Cameras[activeIndex].SetActive(true);
        FindObjectOfType<GameMaster>().MainCamera = Cameras[activeIndex];
    }
}
