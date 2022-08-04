using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerPanel;
    public RouteLogic NodePositions;
    public GameObject PlayerCamera;
    public PlayerDataScriptable PlayerData;
    public List<GameObject> OwnedRegions;//= new List<GameObject>();
    public GameObject master;
    public bool passedBuyEntrancePoint;

    private int CurrentNodePosition;
    public int steps;

    public bool isMoving;
    private float speed=2.5f;

    void Start()
    {
        passedBuyEntrancePoint = false;
        CurrentNodePosition = 29;
        //isMoving = false;
        PlayerData.PlayerPosition = 30;
        PlayerData.PlayerMoney = 2000;
        PlayerPanel.GetComponent<PanelPlayerInfoLogic>().SetPlayerName(PlayerData.PlayerName);
        PlayerPanel.GetComponent<PanelPlayerInfoLogic>().SetPlayerMoney(PlayerData.PlayerMoney);
        OwnedRegions = new List<GameObject>();
        PlayerCamera.SetActive(false);
        //PlayerData.isTurn = false;
    }

    // Update is called once per frame
    void Update()
    {//TEST
       // if (Input.GetKeyDown(KeyCode.Space) && !isMoving) {
        //    steps = Random.Range(1, 7);
        //    Debug.Log("Dice Rolled: " + steps);

       // }
       // StartCoroutine(MovePlayer());
    }
    public void ActivateCamera() {
        PlayerCamera.SetActive(true);
    }

    public void DeactivateCamera()
    {
        PlayerCamera.SetActive(false);
    }
    public void MovePlayerCharacter(int roll) {
        
        PlayerData.PlayerPosition += roll;
        //PlayerData.PlayerPosition %= NodePositions.NodePositionList.Count;
        StartCoroutine(MovePlayer(roll));
    }
    IEnumerator MovePlayer(int steps) {
        isMoving = false;
        if (isMoving) {
            yield break;
        }

        isMoving = true;
        while (steps>0)
        {
            
            CurrentNodePosition++;
            CurrentNodePosition %= NodePositions.NodePositionList.Count;

            //Debug.Log(message: "Position: " + NodePositions.NodePositionList.Count);

            Vector3 NextPos = NodePositions.NodePositionList[CurrentNodePosition].position;
            while (!MoveToNextNode(NextPos))
            {
                yield return null;
            }
            FindObjectOfType<AudioMasterLogic>().PlaySound("Move");
            yield return new WaitForSeconds(0.1f);
            steps--;
            
        }

        isMoving = false;


    }
    public bool MoveToNextNode(Vector3 destination) {
        
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        return destination==(transform.position = Vector3.MoveTowards(transform.position, destination, step));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Money_PassThrough") {
          AddPassMoney();
        }
        else if(other.gameObject.name == "Buy_Entrance"){
            BuyEntranceNode();
        }
    }

    public void AddPassMoney() {
        Debug.Log("Passed A Money Node: ");
        FindObjectOfType<AudioMasterLogic>().PlaySound("GainMoney");
        PlayerPanel.GetComponent<PanelPlayerInfoLogic>().IncreasePlayerMoney(PlayerData.PlayerMoney, PlayerData.PlayerMoney + 200);
        PlayerData.PlayerMoney += 200;
    }
    public void BuyEntranceNode()
    {
        Debug.Log("Passed A Buy Entrance Node: ");
        FindObjectOfType<AudioMasterLogic>().PlaySound("EntrancePass");
        passedBuyEntrancePoint = true;
       // PlayerData.OwnedRegions
    }

   
}
