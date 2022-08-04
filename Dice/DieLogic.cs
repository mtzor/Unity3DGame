using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieLogic : MonoBehaviour
{

    public ViewPanelLogic ViewPanelLogic;
    public GameObject MainCamera;
    public GameObject DieCamera;
    bool mainCameraActive = true;
    bool DieCameraActive = false;

    public GameObject die;
    public Transform diceHolder;
    public Rigidbody m_Rigidbody;
    public int diceValue;
    public DieSides[] dieSides;

    float force = 4f;
    bool thrown;
    bool hasLanded;

    // Start is called before the first frame update
    void Start()
    {
        ResetDie();
       // wasRolled = false;
        //finishedRolling = false;
       // m_Rigidbody = GetComponentInChildren<Rigidbody>();
       // TossDie();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Rigidbody.IsSleeping() && thrown && !hasLanded)
        {
            hasLanded = true;
            m_Rigidbody.useGravity = false;
            SideValueCheck();
        }
        else if (m_Rigidbody.IsSleeping() && hasLanded && diceValue == 0) 
        {
            TossDie();
        }

    }

    public void TossDie() {

        if (thrown && hasLanded) {
            ResetDie();
        }
        FindObjectOfType<AudioMasterLogic>().PlaySound("Dice");
        thrown = true;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(0, 0, 1.5f*force, ForceMode.Impulse);
        m_Rigidbody.AddTorque(0, force, force, ForceMode.Impulse);

    }
    public void ResetDie() {
        die.transform.position = diceHolder.position;
        die.transform.rotation = Random.rotation;
        thrown = false;
        hasLanded = false;
        m_Rigidbody.useGravity = false;
    }
    public void SideValueCheck() {
        diceValue = 0;
        foreach (DieSides side in dieSides)
        {
            if (side.onGround) 
            {
                diceValue = side.DiceValue;
                ViewPanelLogic.FinalRoll = diceValue;
                ViewPanelLogic.wasRolled = true;
                ToggleCameras();
                Debug.Log("Rolled: "+diceValue);
            }
        }
    }
    public void ToggleCameras()
    {
        MainCamera=FindObjectOfType<GameMaster>().MainCamera;
        mainCameraActive =!mainCameraActive;
        DieCameraActive =!DieCameraActive;
        MainCamera.SetActive(mainCameraActive);
        DieCamera.SetActive(DieCameraActive);
    }
}
