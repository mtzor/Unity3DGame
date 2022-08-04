using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] Cameras;
    //public GameObject MainCamera;
    public GameObject DieCamera;
    public GameObject MessagePanel;
    public GameObject ViewPanel;
    public GameObject BuyPanel;
    public GameObject BuildPanel;
    public GameObject EntranceOptionsPanel;
    public GameObject PanelPlayerOptions;
    public GameObject ActivePlayerPanel;
    public GameObject PanelPlayAgain;
    public GameObject PanelTutorial;
    public GameObject GameOverPanel;
    public GameObject[] PlayerList;
    public GameObject[] NodeList;
    public GameObject ConfettiLauncher;
    public GameObject MainCamera;
    GameObject Player1;
    GameObject Player2;
    GameObject CurrentPlayer;
    GameObject OtherPlayer;
    GameObject landedNode;
    int price1;
    int price2;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Is Starting Coroutine: ");
        MainCamera = Cameras[0];
        GamePhase = GamePhase.RollDie;
        Player1 = PlayerList[0];
        Player2 = PlayerList[1];
        Player1.GetComponent<PlayerLogic>().PlayerData.isTurn = false;
        Player2.GetComponent<PlayerLogic>().PlayerData.isTurn = true;
        CurrentPlayer = Player2;
        OtherPlayer = Player1;
        //Debug.Log("Is Starting Coroutine: ");
        BuyPanel.SetActive(false);
        EntranceOptionsPanel.SetActive(false);
        ActivePlayerPanel.GetComponent<PanelActivePlayerLogic>().SetActivePlayerName(CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerName);
      
        StartCoroutine(RunGame());
    }

    // Update is called once per frame
    void Update()
    {

    }
    GamePhase GamePhase;
    IEnumerator RunGame()
    {
        MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage("Welcome to MuseumLand! \n Here are a few tips on how to play the game!");
        MessagePanel.SetActive(true);
        yield return new WaitForSeconds(6f);
        MessagePanel.SetActive(false);

        PanelTutorial.SetActive(true);
        //Debug.Log("Is Running: ");
        while (GamePhase != GamePhase.EndGame) 
        {
            yield return new WaitForSeconds(1f);
            #region Show Whos' Turn it is
            MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage(CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerName + "'s turn!");
            MessagePanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            MessagePanel.SetActive(false);
            #endregion
            yield return new WaitForSeconds(1f);

            #region RollDie
            if (GamePhase == GamePhase.RollDie) {
                
                EnableRollDie("Roll Die To Move!");//shows panel to roll die

                #region Wait For Roll
                ViewPanel.GetComponent<ViewPanelLogic>().wasRolled = false;
              
                while (!ViewPanel.GetComponent<ViewPanelLogic>().wasRolled)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                DieResult = ViewPanel.GetComponent<ViewPanelLogic>().FinalRoll;
                #endregion

                /*
                #region Show Die Result
                yield return new WaitForSeconds(1f);
                MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage("You rolled: " + DieResult.ToString());
                MessagePanel.SetActive(true);
                yield return new WaitForSeconds(2f);
                MessagePanel.SetActive(false);
                #endregion
                */
                DisableRollDie();//disables roll die panel

                yield return new WaitForSeconds(1f);
                GamePhase = GamePhase.Move;


            }
            #endregion

            #region Move
            if (GamePhase == GamePhase.Move)
            {
                #region Toggle Cameras
                MainCamera.SetActive(false);
                CurrentPlayer.GetComponent<PlayerLogic>().ActivateCamera();
                #endregion

                #region Get Info for Move
                int playerPos = CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerPosition;
                int targetpos = (playerPos + DieResult);
                GameObject startNode = NodeList[(playerPos-1) % NodeList.Length];//saying the start node has no longer a player on it
                startNode.GetComponent<NodeLogic>().NodeData.HasPlayer = false;//saying the start node has no longer a player on it
                Debug.Log("Target Node is: "+ targetpos % NodeList.Length);
                GameObject targetNode = NodeList[(targetpos - 1) % NodeList.Length];
                #endregion

                #region Start Moving
                if (targetNode.GetComponent<NodeLogic>().NodeData.HasPlayer) {
                    CurrentPlayer.GetComponent<PlayerLogic>().MovePlayerCharacter(DieResult + 1);
                }
                else {
                  CurrentPlayer.GetComponent<PlayerLogic>().MovePlayerCharacter(DieResult);
                 // CurrentPlayer.GetComponent<PlayerLogic>().MovePlayerCharacter(6);
                }
                #endregion

                #region Wait For Move Completion
                while (CurrentPlayer.GetComponent<PlayerLogic>().isMoving)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                targetNode.GetComponent<NodeLogic>().NodeData.HasPlayer = true;//the node is now occupied
                #endregion

                #region Toggle Cameras
                CurrentPlayer.GetComponent<PlayerLogic>().DeactivateCamera();
                MainCamera.SetActive(true);
                #endregion

                GamePhase = GamePhase.RoadAction;

            }
            #endregion

            #region Buy Entrance Pass Point
            //buy entrance passpoint
            if (CurrentPlayer.GetComponent<PlayerLogic>().passedBuyEntrancePoint) 
            {

                CurrentPlayer.GetComponent<PlayerLogic>().passedBuyEntrancePoint = false;//reset the passpoint indicator

                //would you like to buy an entrance? MENU
                #region Show Entrance Menu
                EntranceOptionsPanel.SetActive(true);
                EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().ActivateChoicePanel();
                #endregion

                #region Wait For Menu Choice
                EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosenPlace = false;
                while (!EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosenPlace)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().DeactivateChoicePanel();
                #endregion

                int panelchoice = EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().placeChoice;
                #region Chose To Place Entrance
                if (panelchoice == 0)//if chose to place entrance
                {
                    #region Toggle Cameras
                    MainCamera.SetActive(false);
                    EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().ActivateCamera();
                    #endregion

                    #region Get Entrance Choice
                    EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosen = false;
                    ShowEntranceOptions();
                    //show message that says chose ????????????????????? place more than one options

                    //activate the adequate entances
                    while (!EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosen)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                    HideEntranceOptions();

                    #endregion

                    int choice = EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().choice;
                    RegionType Entrychoice = EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().Entrancetype;
                    // Debug.Log("Chose Node: " + choice + "Which ? :" + Entrychoice);
                    #region Choce Entrance => Place and Pay 
                    if (choice != -1)
                    {
                        ShowCurrentPlayerInfo(CurrentPlayer);//??????????
                        PlaceAndPayEntrance(choice, Entrychoice);
                        ShowCurrentPlayerInfo(CurrentPlayer);//???????????
                    }
                    #endregion

                    #region Can Not Place=> Abort
                    if (choice == -1)//does noto have plots qualified for entrance
                    {
                        EntranceOptionsPanel.SetActive(false);
                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().DeactivateCamera();
                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().ActivatePlayerPanels();
                        MainCamera.SetActive(true);
                        MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage("Cannot place any entrances!");
                        MessagePanel.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        MessagePanel.SetActive(false);
                    }
                    #endregion

                  
                }
                #endregion
                    #region Toggle Cameras
                    EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().DeactivateCamera();
                    EntranceOptionsPanel.SetActive(false);
                    MainCamera.SetActive(true);
                    #endregion
            }
            #endregion

            #region RoadAction
            if (GamePhase == GamePhase.RoadAction)
            {
                yield return new WaitForSeconds(1f);
                #region Get Landed Node Data
                int pos = CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerPosition;//get player position
                Debug.Log("Pos: "+ pos % NodeList.Length);
                landedNode = NodeList[(pos - 1) % NodeList.Length];//find the nodde responding to the position from the noded array
                NodeDataScriptable landedNodeData = landedNode.GetComponent<NodeLogic>().NodeData;
                NodeType nodetype = landedNodeData.NodeType;
                GameObject EntryRegion=landedNode.GetComponent<NodeLogic>().AdjacentRegions[landedNodeData.EntryRegion];
                #endregion

                #region Node Has Entrance To A Museum of The Oponent
                if (landedNodeData.HasEntry && EntryRegion.GetComponent<RegionLogic>().owner!=CurrentPlayer) 
                {
                    int payDieResult = 1;
                    FindObjectOfType<AudioMasterLogic>().PlaySound("Pay");
                    RegionDataScriptable regDat=EntryRegion.GetComponent<RegionLogic>().RegionData;
                    PlayerDataScriptable PlayerDat = CurrentPlayer.GetComponent<PlayerLogic>().PlayerData;
                    #region Show Die Roll Menu
                    EnableRollDie("You have landed on the entrance of "+regDat.MuseumName +" !\n The ticket price is "+regDat.TicketPrices[regDat.BuiltExhibits-1]+"$.\n Roll die to see how many tickets you will buy!");
                    Debug.Log("Enabled DieRoll: ");
                    #endregion

                    #region Wait For Die Result
                    ViewPanel.GetComponent<ViewPanelLogic>().wasRolled = false;
                    while (!ViewPanel.GetComponent<ViewPanelLogic>().wasRolled)//set false first maybe???
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                    payDieResult = ViewPanel.GetComponent<ViewPanelLogic>().FinalRoll;
                    #endregion

                    DisableRollDie();//disable die roll menu
                    yield return new WaitForSeconds(1f);

                    PayPlayer(EntryRegion,payDieResult);///Pay Player ????????????????
                }
                #endregion

                #region Node Action
                else
                {
                    #region Build Node
                    if (nodetype == NodeType.Build)
                    {
                        #region Activate Build Choice Menu
                        BuildPanel.SetActive(true);
                        BuiltExhibitRegionChoice();
                        #endregion

                        ShowCurrentPlayerInfo(CurrentPlayer);///?????????????

                        #region Wait And Get Player Choice
                        BuildPanel.GetComponent<PanelBuitExhibitLogic>().hasChosen = false;
                        while (!BuildPanel.GetComponent<PanelBuitExhibitLogic>().hasChosen)
                        {
                            yield return new WaitForSeconds(0.1f);
                        }
                        int choice = BuildPanel.GetComponent<PanelBuitExhibitLogic>().option;
                        #endregion

                        CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().TogglePlayerMuseums(false);

                        if (OwnedRegions.Count != 0 && choice!=0) //if player can build
                        {
                            //Menu saying please Roll Build Die
                            #region Show Menu To Roll Build Die
                            PanelPlayerOptions.GetComponent<PanelPlayerOptionsLogic>().EnableRollBuildDieButton();
                            BuildPanel.SetActive(false);
                            //ViewPanel.GetComponent<ViewPanelLogic>().ShowPlayerInfo("ROLL BUILD DIE!");
                            #endregion

                            #region Get Build Die Result
                            ViewPanel.GetComponent<ViewPanelLogic>().wasBuildRolled = false;
                            //roll if 2x sucess or free then charge
                            while (!ViewPanel.GetComponent<ViewPanelLogic>().wasBuildRolled)
                            {
                                yield return new WaitForSeconds(0.1f);
                            }
                            yield return new WaitForSeconds(1f);
                            #endregion

                            #region Deactivating Menus
                            PanelPlayerOptions.GetComponent<PanelPlayerOptionsLogic>().DisableRollBuildDieButton();
                            BuildPanel.SetActive(false);
                            #endregion

                            BuildDiceFaces result = ViewPanel.GetComponent<ViewPanelLogic>().BuildDiceResult;//get roll result
                            /*
                            #region Show Die Result
                            yield return new WaitForSeconds(1f);
                            MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage("You rolled: "+result.ToString());
                            MessagePanel.SetActive(true);
                            yield return new WaitForSeconds(2f);
                            MessagePanel.SetActive(false);
                            #endregion
                            */
                            #region IF not Denied Charge For Exhibit
                            if (result!=BuildDiceFaces.Fail) {
                                MainCamera.SetActive(false);
                                ChargeForExhibit(choice, result);//????????????
                                yield return new WaitForSeconds(3f);
                                ChosenRegion.GetComponent<RegionLogic>().DeactivateRegionCamera();
                                MainCamera.SetActive(true);
                                yield return new WaitForSeconds(3f);
                            }
                            #endregion


                            ShowCurrentPlayerInfo(CurrentPlayer);//????????????
                            yield return new WaitForSeconds(1f);
                            CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().TogglePlayerMuseums(false);

                        }
                        #region  Chose to Build But Cannot
                        else if (OwnedRegions.Count == 0 && choice != 0)
                        {
                            MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage("All exhibits of your museums are built!");
                            MessagePanel.SetActive(true);
                            yield return new WaitForSeconds(2f);
                            MessagePanel.SetActive(false);

                        }
                        #endregion

                        BuildPanel.SetActive(false);
                        
                    }
                    #endregion

                    #region Free Build Node
                    else if (nodetype == NodeType.FreeBuild)
                    {
                        #region Show  Build Menus
                        BuildPanel.SetActive(true);
                        BuiltFreeExhibitRegionChoice();
                        #endregion

                        ShowCurrentPlayerInfo(CurrentPlayer);
                        #region Wait For Build Choice
                        BuildPanel.GetComponent<PanelBuitExhibitLogic>().hasChosen = false;
                        while (!BuildPanel.GetComponent<PanelBuitExhibitLogic>().hasChosen)
                        {
                            yield return new WaitForSeconds(0.1f);
                        }
                        #endregion

                        #region If chose Build Charge
                        int choice = BuildPanel.GetComponent<PanelBuitExhibitLogic>().option;

                        Debug.Log("Chose: " + choice);
                        if (choice != 0)
                        {
                            MainCamera.SetActive(false);
                            ChargeForExhibit(choice, BuildDiceFaces.Free);                    
                            yield return new WaitForSeconds(3f);
                            ChosenRegion.GetComponent<RegionLogic>().DeactivateRegionCamera();
                            MainCamera.SetActive(true);

                        }
                        #endregion

                        #region Panel Toggling
                        CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().TogglePlayerMuseums(false);
                        BuildPanel.SetActive(false);
                        #endregion
                    }
                    #endregion

                    #region Free Entrance Node
                    else if (nodetype == NodeType.FreeEntrance) 
                    {
                        #region Toggle  Menus Entrance Choice (PLace or not)
                        /////??????? add text saying choose entrance and money
                        EntranceOptionsPanel.SetActive(true);
                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().ActivateChoiceFreePanel();
                        #endregion

                        #region Wait For Entrance Choice
                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosenPlace = false;
                        while (!EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosenPlace)
                        {
                            yield return new WaitForSeconds(0.1f);
                        }
                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().DeactivateChoiceFreePanel();
                        #endregion

                        #region Toggle Cameras
                        MainCamera.SetActive(false);
                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().ActivateCamera();
                        #endregion

                        int panelchoice = EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().placeChoice;
                        Debug.Log("Chose :" + panelchoice);

                        #region Chose Place
                        if (panelchoice==0) 
                        {
                            EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosen = false;
                            ShowEntranceOptions();
                            //show message that says chose
                            #region Wait For Entrance Choice
                            while (!EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosen)
                                {
                                    yield return new WaitForSeconds(0.1f);
                                }
                            #endregion

                            HideEntranceOptions();

                            int choice = EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().choice;
                            RegionType Entrychoice = EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().Entrancetype;

                            #region Place Chosen Entrance
                                if (choice != -1) {
                                    PlaceEntrance(choice, Entrychoice);
                                }
                            #endregion

                            #region Cannot Place
                            if (choice == -1)//cannot place entrances
                                {
                                    EntranceOptionsPanel.SetActive(false);
                                    EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().DeactivateCamera();
                                    EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().ActivatePlayerPanels();
                                    MainCamera.SetActive(true);
                                    MessagePanel.GetComponent<PanelMessagesLogic>().SetMessage("Cannot place any entrances!");
                                    MessagePanel.SetActive(true);
                                    yield return new WaitForSeconds(1f);
                                    MessagePanel.SetActive(false);
                                }
                            #endregion
                        }
                        #endregion

                        EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().DeactivateCamera();
                        EntranceOptionsPanel.SetActive(false);

                        MainCamera.SetActive(true);
                    }
                    #endregion

                    #region Buy Node
                    else if (nodetype == NodeType.Buy)
                    {
                        #region Activate Panels And Menus
                        BuyPanel.SetActive(true);
                        ShowBuyMenu();
                        #endregion

                        #region Wait For Choice
                        BuyPanel.GetComponent<PanelBuyOptions>().hasChosen = false;
                        while (!BuyPanel.GetComponent<PanelBuyOptions>().hasChosen)
                        {
                            yield return new WaitForSeconds(0.1f);
                        }

                        int choice = BuyPanel.GetComponent<PanelBuyOptions>().option;
                        #endregion

                        ChargeForRegion(choice);//?????????????
                        if (choice != 3) {  yield return new WaitForSeconds(3f);}
                       

                        #region Show Owned Museums
                        //yield return new WaitForSeconds(0.5f);
                        CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().TogglePlayerMuseums(false);
                        OtherPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().TogglePlayerMuseums(false);
                        #endregion

                        BuyPanel.SetActive(false);
                    }
                    #endregion
                }
                #endregion

                #region IF GameOver
                if (CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney <= 0)
                {
                    ConfettiLauncher.SetActive(true);
                    GamePhase = GamePhase.EndGame;
                    SwitchPlayers();
                    ViewPanel.SetActive(false);
                    FindObjectOfType<AudioMasterLogic>().PlaySound("Win");
                    GameOverPanel.GetComponent<GameOverPanelLogic>().SetWinnerText(CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerName);
                    GameOverPanel.SetActive(true);
                    //show gameover panel
                }
                #endregion

                #region Player Switching
                else if(DieResult!=6)
                {
                    yield return new WaitForSeconds(1f);
                    SwitchPlayers();
                    FindObjectOfType<AudioMasterLogic>().PlaySound("ChangeTurn");
                    ActivePlayerPanel.GetComponent<PanelActivePlayerLogic>().SetActivePlayerName(CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerName);
                    GamePhase = GamePhase.RollDie;
                }
                else if (DieResult==6)
                {
                    PanelPlayAgain.SetActive(true);
                    PanelPlayAgain.GetComponent<PanelPlayAgainLogic>().TogglePanel(true);
                    PanelPlayAgain.GetComponent<PanelPlayAgainLogic>().hasChosen = false;

                    while (!PanelPlayAgain.GetComponent<PanelPlayAgainLogic>().hasChosen)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                    PanelPlayAgain.SetActive(false);
                    bool chosePlay;
                    chosePlay = PanelPlayAgain.GetComponent<PanelPlayAgainLogic>().play;

                    if (!chosePlay)
                    {
                        yield return new WaitForSeconds(1f);
                        SwitchPlayers();
                        FindObjectOfType<AudioMasterLogic>().PlaySound("ChangeTurn");
                        ActivePlayerPanel.GetComponent<PanelActivePlayerLogic>().SetActivePlayerName(CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerName);
                    }
                    GamePhase = GamePhase.RollDie;
                }
                #endregion
            }
            #endregion

        }

    }

    private void HideEntranceOptions()
    {
        List<GameObject> OwnedRegions = CurrentPlayer.GetComponent<PlayerLogic>().OwnedRegions;

        if (OwnedRegions.Count != 0)//if player owns regions
        {  //write correct text on Buttons
           // Debug.Log("How many Regions :" + OwnedRegions.Count);
            for (int i = 0; i < OwnedRegions.Count; i++)// for each region
            {

                GameObject region = OwnedRegions[i];
                RegionDataScriptable RegData = region.GetComponent<RegionLogic>().RegionData;
               
                region.GetComponent<RegionLogic>().DeactivateEntrances();
            
            }
         
        }

    }

    private void PlaceEntrance(int choice, RegionType entrychoice)
    {
        string color="red";
        GameObject ChosenNode = NodeList[choice - 1];//chosen node to put entrance
        if (CurrentPlayer == Player2)
        {
            color = "blue";
            Debug.Log("blue");
        }

        if (entrychoice == RegionType.Inner)
        {
            ChosenNode.GetComponent<NodeLogic>().ActivateInnerEntrance(color);
            if (ChosenNode.GetComponent<NodeLogic>().AdjacentRegions.Length != 1)
            {
                ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion = 1;
            }
            else
            {
                ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion = 0;
            }

        }
        else if (entrychoice == RegionType.Outer)
        {

            ChosenNode.GetComponent<NodeLogic>().ActivateOuterEntrance(color);
            ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion = 0;
        }
        ChosenNode.GetComponent<NodeLogic>().NodeData.HasEntry = true;
   
    }
    private void PlaceAndPayEntrance(int choice, RegionType entrychoice)
    {
        string color = "red";
        GameObject ChosenNode = NodeList[choice - 1];//chosen node to put entrance
        if (CurrentPlayer == Player2)
        {
            color = "blue";
            Debug.Log("blue");
        }
        if (entrychoice==RegionType.Inner)
        {
            ChosenNode.GetComponent<NodeLogic>().ActivateInnerEntrance(color);
            if (ChosenNode.GetComponent<NodeLogic>().AdjacentRegions.Length != 1) {
                ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion = 1;
            }
            else
            {
                ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion = 0;
            }
            
        }
        else if(entrychoice == RegionType.Outer){ 

            ChosenNode.GetComponent<NodeLogic>().ActivateOuterEntrance(color);
            ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion = 0;
        }
        ChosenNode.GetComponent<NodeLogic>().NodeData.HasEntry = true;
        //get the chosen region as the region adjacent to the node with index shown by entryregion
        GameObject ChosenRegion = ChosenNode.GetComponent<NodeLogic>().AdjacentRegions[ChosenNode.GetComponent<NodeLogic>().NodeData.EntryRegion];
        int money= CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;
        int RegPrice= ChosenRegion.GetComponent<RegionLogic>().RegionData.EntrancePrice;

        CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().ReducePlayerMoney(money, money -RegPrice);
        CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney -= RegPrice;
        Debug.Log("Paid:" + ChosenRegion.GetComponent<RegionLogic>().RegionData.EntrancePrice);
    }

    private void ShowEntranceOptions()
    {
        List<GameObject> OwnedRegions = CurrentPlayer.GetComponent<PlayerLogic>().OwnedRegions;
        int noExhibitRegions = 0;
        
        if (OwnedRegions.Count != 0)//if player owns regions
        {  //write correct text on Buttons
            Debug.Log("How many Regions :" + OwnedRegions.Count);
            for (int i = 0; i < OwnedRegions.Count; i++)// for each region
            {
                
                GameObject region = OwnedRegions[i];
                RegionDataScriptable RegData = region.GetComponent<RegionLogic>().RegionData;
                if (RegData.BuiltExhibits > 0)//if it has buildings activate buttons
                {
                    region.GetComponent<RegionLogic>().ActivateEntrances();
                }
                else {//else say it does not
                    noExhibitRegions++;
                }
            }
            if (noExhibitRegions == OwnedRegions.Count)//if all owned regions dont have exhibits we dont need to choose
            {
                EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().choice = -1;
                EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosen = true;
            }
        }
        else
        {
            EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().choice = -1;
            EntranceOptionsPanel.GetComponent<EntranceOptionsLogic>().hasChosen = true;
        }


    }

    int DieResult = 0;
    bool GotResult = false;
    GameObject AdjecentRegion1;
    GameObject AdjecentRegion2;
    RegionDataScriptable RegData1;
    RegionDataScriptable RegData2;
    List<GameObject> OwnedRegions;
    GameObject ChosenRegion;
    //bool NoOwnedRegions = false;
    #region Build-Build Free
    public void BuiltFreeExhibitRegionChoice()
    {
        List<GameObject> OwnedRegions = CurrentPlayer.GetComponent<PlayerLogic>().OwnedRegions;
        string[] names = new string[7]; ;//new string[OwnedRegions.Count];
        string[] texts = new string[7];

        BuildPanel.GetComponent<PanelBuitExhibitLogic>().ShowFreeMessage();
        //Deactivate Not Used Buttons
        ActivateBuildButtons(OwnedRegions.Count);

        if (OwnedRegions.Count != 0)
        {  //write correct text on Buttons
            for (int i = 0; i < OwnedRegions.Count; i++)
            {
                GameObject region = OwnedRegions[i];
                RegionDataScriptable RegData = region.GetComponent<RegionLogic>().RegionData;

                names[i] = RegData.MuseumName;
                if (RegData.BuiltExhibits < 4)
                {
                    texts[i] = names[i] ;
                }
                else
                {
                    BuildPanel.GetComponent<PanelBuitExhibitLogic>().DectivateChoice(i+1);
                }


            }

            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ShowButtons(texts);
       
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateBuildOrNotMenu();
        }


    }

    public void ActivateBuildButtons(int OwnedRegions) {
        if (OwnedRegions == 0)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().NoOwnedRegion();
            //NoOwnedRegions = true;
            return;
        }
        else if (OwnedRegions == 1)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 0, 0, 0, 0, 0, 0);

        }
        else if (OwnedRegions == 2)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 1, 0, 0, 0, 0, 0);
        }
        else if (OwnedRegions == 3)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 1, 1, 0, 0, 0, 0);
        }
        else if (OwnedRegions == 4)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 1, 1, 1, 0, 0, 0);
        }
        else if (OwnedRegions == 5)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 1, 1, 1, 1, 0, 0);
        }
        else if (OwnedRegions == 6)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 1, 1, 1, 1, 1, 0);
        }
        else if (OwnedRegions == 7)
        {
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateButton1();
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ToggleButtons(1, 1, 1, 1, 1, 1, 1);
        }
    }
    public void BuiltExhibitRegionChoice() {
        OwnedRegions = CurrentPlayer.GetComponent<PlayerLogic>().OwnedRegions;
        Debug.Log("Region Count: "+OwnedRegions.Count);
        string[] names= new string[7]; ;//new string[OwnedRegions.Count];
        string[] prices= new string[7]; //new string[OwnedRegions.Count];
        string[] texts = new string[7];

        BuildPanel.GetComponent<PanelBuitExhibitLogic>().ShowMessage();
        //Deactivate Not Used Buttons
        ActivateBuildButtons(OwnedRegions.Count);

            if (OwnedRegions.Count != 0)
        {  //write correct text on Buttons
            for (int i = 0; i < OwnedRegions.Count; i++) {
                GameObject region = OwnedRegions[i];
                RegionDataScriptable RegData = region.GetComponent<RegionLogic>().RegionData;

                names[i] = RegData.MuseumName;
                if (RegData.BuiltExhibits < 4) {
                    prices[i] = RegData.ExhibitPrices[RegData.BuiltExhibits].ToString();
                    texts[i] = names[i] + ": " + prices[i] + "$";
                }
                else {
                    Debug.Log("Deactivating: ");
                    BuildPanel.GetComponent<PanelBuitExhibitLogic>().DectivateChoice(i+1);
                }
               

            }

            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ShowButtons(texts);
            BuildPanel.GetComponent<PanelBuitExhibitLogic>().ActivateBuildOrNotMenu();
        }

        
    }
    public void ChargeForExhibit(int choice,BuildDiceFaces result)
    {
         List< GameObject > OwnedRegions= CurrentPlayer.GetComponent<PlayerLogic>().OwnedRegions;
        if (OwnedRegions.Count!=0) { 
         ChosenRegion = OwnedRegions[choice - 1];
         Debug.Log("Chose: " + ChosenRegion.GetComponent<RegionLogic>().RegionData.MuseumName);

        RegionDataScriptable RegData = ChosenRegion.GetComponent<RegionLogic>().RegionData;
        if (RegData.BuiltExhibits < 4)
        {
            int price = RegData.ExhibitPrices[RegData.BuiltExhibits];
            int money = CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;

            if (result == BuildDiceFaces.Success) {//if succeded
                CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().ReducePlayerMoney(money,money-price);
                CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney -= price;
                
                }
            else if (result == BuildDiceFaces.Double)//if double cost
            {
                    CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().ReducePlayerMoney(money, money -2* price);
                    CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney -= 2 * price;
            }
            //no cost skips if does not charge player
            ChosenRegion.GetComponent<RegionLogic>().ActivateExhibit(RegData.BuiltExhibits + 1);
            RegData.BuiltExhibits += 1;
            }
        }
    }
    #endregion

    #region Buy
    public void ShowBuyMenu()
    {
        //Show buy menu
         AdjecentRegion1 = landedNode.GetComponent<NodeLogic>().AdjacentRegions[0];//find adjecent regions of node
         AdjecentRegion2 = landedNode.GetComponent<NodeLogic>().AdjacentRegions[1];// find adjecent regions of node
         RegData1 = AdjecentRegion1.GetComponent<RegionLogic>().RegionData;
        RegData2 = AdjecentRegion2.GetComponent<RegionLogic>().RegionData;
        string reg1Name = RegData1.MuseumName;//get their name
        string reg2Name = RegData2.MuseumName;//get name
        price1 = RegData1.ByingPrice;
        price2 = RegData2.ByingPrice;

        string text1 = reg1Name + ": " + price1.ToString() + "$"; ;
        string text2 = reg2Name + ": " + price2.ToString() + "$";
        BuyPanel.GetComponent<PanelBuyOptions>().SetMessage("You have landed on a buy node!\n What would you like to do? ");
        //region owned by someone else no buildings
        if (RegData1.HasOwner && AdjecentRegion1.GetComponent<RegionLogic>().owner!=CurrentPlayer&& RegData1.BuiltExhibits == 0)
        {
            price1 /= 2;
            text1 = reg1Name + ": " + price1.ToString() + "$";
            BuyPanel.GetComponent<PanelBuyOptions>().ActivateButton1();

        }
        //region owned by someone else with one or more buildings
        else if (RegData1.HasOwner && AdjecentRegion1.GetComponent<RegionLogic>().owner != CurrentPlayer && RegData1.BuiltExhibits > 0)
        {

            BuyPanel.GetComponent<PanelBuyOptions>().DeactivateButton1();
        }
        else if(!RegData1.HasOwner)//region not owned
        {
            text1 = reg1Name + ": " + price1.ToString() + "$";
            BuyPanel.GetComponent<PanelBuyOptions>().ActivateButton1();
        }
        else//region owned by you
        {
            BuyPanel.GetComponent<PanelBuyOptions>().DeactivateButton1();
            Debug.Log("else: " + price1);
        }

        //region owned by someone else no buildings
        if (RegData2.HasOwner && AdjecentRegion2.GetComponent<RegionLogic>().owner != CurrentPlayer && RegData2.BuiltExhibits == 0)
        {
            price2 /= 2;
            text2 = reg2Name + ": " + price2.ToString() + "$";
            BuyPanel.GetComponent<PanelBuyOptions>().ActivateButton2();
        }
        //region owned by someone else with one or more buildings
        else if (RegData2.HasOwner && AdjecentRegion2.GetComponent<RegionLogic>().owner != CurrentPlayer && RegData2.BuiltExhibits > 0)
        {

            BuyPanel.GetComponent<PanelBuyOptions>().DeactivateButton2();
        }
        else if (!RegData2.HasOwner)// does not have owner
        {
            text2 = reg2Name + ": " + price2.ToString() + "$";
            BuyPanel.GetComponent<PanelBuyOptions>().ActivateButton2();
        }
        else {
            BuyPanel.GetComponent<PanelBuyOptions>().DeactivateButton2();
        }

        bool cannotBuy = false;
        bool Available1 = false;
        bool Available2 = false;
        Available2 = (RegData2.HasOwner && AdjecentRegion2.GetComponent<RegionLogic>().owner == CurrentPlayer) || (RegData2.HasOwner &&
                    AdjecentRegion2.GetComponent<RegionLogic>().owner != CurrentPlayer && RegData2.BuiltExhibits > 0);
        Available1= (RegData1.HasOwner && AdjecentRegion1.GetComponent<RegionLogic>().owner == CurrentPlayer) || (RegData1.HasOwner &&
                    AdjecentRegion1.GetComponent<RegionLogic>().owner != CurrentPlayer && RegData1.BuiltExhibits > 0);
        cannotBuy = Available1 && Available2;
        if (cannotBuy) 
        { 
        //both owned
        Debug.Log("ELSE: " + price1);
        BuyPanel.GetComponent<PanelBuyOptions>().SetMessage("Cannot buy any regions!");
        text2 = "OK";
        }

        BuyPanel.GetComponent<PanelBuyOptions>().ShowButtons(text1, text2);
        BuyPanel.GetComponent<PanelBuyOptions>().ActivateBuyMenu();//activating buy menu ui

       // Debug.Log("Price is: " + price1);

        // CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney-=

    }

    public void ChargeForRegion(int choice) {
        AdjecentRegion1 = landedNode.GetComponent<NodeLogic>().AdjacentRegions[0];//find adjecent regions of node
        AdjecentRegion2 = landedNode.GetComponent<NodeLogic>().AdjacentRegions[1];// find adjecent regions of node
        RegData1 = AdjecentRegion1.GetComponent<RegionLogic>().RegionData;
        RegData2 = AdjecentRegion2.GetComponent<RegionLogic>().RegionData;
        GameObject ChosenRegion=null;
        RegionDataScriptable ChosenRegionData=null;
        int price = 0; ;

        Debug.Log("Region name is: " + RegData2.MuseumName);
        int money = CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;
        int omoney = OtherPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;

        if (choice == 1) {
            ChosenRegion = AdjecentRegion1;
            ChosenRegionData = RegData1;
            price = price1;
        }
        else if (choice == 2)
        {
            ChosenRegion = AdjecentRegion2;
            ChosenRegionData = RegData2;
            price = price2;
        }
        if(choice==1 || choice == 2) { 

        ShowCurrentPlayerInfo(CurrentPlayer);
        CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().ReducePlayerMoney(money, money - price);
        CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney -= price;
        if (ChosenRegionData.HasOwner == true)
        {
            OtherPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().IncreasePlayerMoney(omoney, omoney + price);
            ChosenRegion.GetComponent<RegionLogic>().owner.GetComponent<PlayerLogic>().PlayerData.PlayerMoney += price;
            ShowCurrentPlayerInfo(ChosenRegion.GetComponent<RegionLogic>().owner);
            ChosenRegion.GetComponent<RegionLogic>().owner.GetComponent<PlayerLogic>().OwnedRegions.Remove(ChosenRegion);
            ShowCurrentPlayerInfo(ChosenRegion.GetComponent<RegionLogic>().owner);
        }
        ChosenRegionData.HasOwner = true;
        ChosenRegion.GetComponent<RegionLogic>().owner = CurrentPlayer;
        CurrentPlayer.GetComponent<PlayerLogic>().OwnedRegions.Add(ChosenRegion);
        ShowCurrentPlayerInfo(CurrentPlayer);
        }
        
    }

    #endregion

    #region ButtonToggling
    public void EnableRollDie(string text) {
      
        PanelPlayerOptions.GetComponent<PanelPlayerOptionsLogic>().EnableRollDieButton(text);
    }
    public void DisableRollDie()
    {
        PanelPlayerOptions.GetComponent<PanelPlayerOptionsLogic>().DisableRollDieButton();
    }
    public void EnableRollBuildDie()
    {
        PanelPlayerOptions.GetComponent<PanelPlayerOptionsLogic>().EnableRollBuildDieButton();
    }
    public void DisableRollBuildDie()
    {
        PanelPlayerOptions.GetComponent<PanelPlayerOptionsLogic>().DisableRollBuildDieButton();
    }
    #endregion

    bool MoveEnded = false;

    public object ConfettiLaucher { get; private set; }

    public void SwitchPlayers() {
        if (CurrentPlayer == Player1)
        {
            CurrentPlayer = Player2;
            OtherPlayer = Player1;
        }
        else
        {
            OtherPlayer = Player2;
            CurrentPlayer = Player1;
        }
    }
    public void PayPlayer(GameObject EntryRegion, int roll)
    {
        int money = CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;
        int omoney = OtherPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;

        RegionDataScriptable EntryRegionData = EntryRegion.GetComponent<RegionLogic>().RegionData;
        int pricePerTicket = EntryRegionData.TicketPrices[EntryRegionData.BuiltExhibits-1];//single ticket price based on built exhibits
        int finalPrice = pricePerTicket * roll;

        GameObject Owner=EntryRegion.GetComponent<RegionLogic>().owner;
        //show current player data
        ShowCurrentPlayerInfo(Owner);
        CurrentPlayer.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().ReducePlayerMoney(money, money - finalPrice);
        CurrentPlayer.GetComponent<PlayerLogic>().PlayerData.PlayerMoney -= finalPrice;
        Owner.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().IncreasePlayerMoney(omoney, omoney + finalPrice);
        Owner.GetComponent<PlayerLogic>().PlayerData.PlayerMoney += finalPrice;
        //Show current player data
        ShowCurrentPlayerInfo(Owner);

    }
    public void ShowCurrentPlayerInfo(GameObject player) {
        string name = player.GetComponent<PlayerLogic>().PlayerData.PlayerName;
        int money = player.GetComponent<PlayerLogic>().PlayerData.PlayerMoney;
        string[] names = new string[7];
        string texts = "";
        List<GameObject> OwnedReg = player.GetComponent<PlayerLogic>().OwnedRegions;

        if (OwnedReg.Count != 0)
        {  //write correct text on Buttons
            for (int i = 0; i < OwnedReg.Count; i++)
            {
                GameObject region = OwnedReg[i];
                RegionDataScriptable RegData = region.GetComponent<RegionLogic>().RegionData;

                names[i] = RegData.MuseumName;
                texts = texts + names[i] + "\n ";

            }

        }
        player.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().SetPlayerMoney(money);
        player.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().SetPlayerName(name);
        player.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().SetPlayerRegions(texts);
        player.GetComponent<PlayerLogic>().PlayerPanel.GetComponent<PanelPlayerInfoLogic>().TogglePlayerMuseums(true);
       
    }

}





public enum GamePhase
{
    RollDie, Move, PassThroughAction,RoadAction,SwitchPlayer,EndGame
}
