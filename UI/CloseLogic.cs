using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLogic : MonoBehaviour
{
    public GameObject QuitPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseApp()
    {
        QuitPanel.SetActive(true);
    }

    public void ChoseQuit()
    {
        Application.Quit();
    }

    public void ChoseNotQuit()
    {
        QuitPanel.SetActive(false);
    }
}
