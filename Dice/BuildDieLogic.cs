using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDieLogic : MonoBehaviour
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

    float force = 7f;
    bool thrown;
    bool hasLanded;

    // Start is called before the first frame update
    void Start()
    {
        ResetDie();

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

    public void TossDie()
    {

        if (thrown && hasLanded)
        {
            ResetDie();
        }
        FindObjectOfType<AudioMasterLogic>().PlaySound("Dice");
        thrown = true;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(force, 0, force, ForceMode.Impulse);
        m_Rigidbody.AddTorque(0, force, force, ForceMode.Impulse);

    }
    public void ResetDie()
    {
        die.transform.position = diceHolder.position;
        die.transform.rotation = Random.rotation;
        thrown = false;
        hasLanded = false;
        m_Rigidbody.useGravity = false;
    }
    public void SideValueCheck()
    {
        diceValue = 0;
        foreach (DieSides side in dieSides)
        {
            if (side.onGround)
            {
                diceValue = side.DiceValue;
                if (diceValue ==1)//free
                {
                    ViewPanelLogic.BuildDiceResult = BuildDiceFaces.Free;
                }
                else if (diceValue == 4)//double
                {
                    ViewPanelLogic.BuildDiceResult = BuildDiceFaces.Double;
                }
                else if (diceValue== 3)//denied
                {
                    ViewPanelLogic.BuildDiceResult = BuildDiceFaces.Fail;
                }
                else //Free
                {
                    ViewPanelLogic.BuildDiceResult = BuildDiceFaces.Success;
                }
                ViewPanelLogic.wasBuildRolled = true;
                ToggleCameras();
                Debug.Log("Rolled: " + diceValue);
            }
        }
    }
    public void ToggleCameras()
    {
        MainCamera = FindObjectOfType<GameMaster>().MainCamera;
        mainCameraActive = !mainCameraActive;
        DieCameraActive = !DieCameraActive;
        MainCamera.SetActive(mainCameraActive);
        DieCamera.SetActive(DieCameraActive);
    }
}
