using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewPlayerDataObj", menuName = "myObjects/PlayerData")]
public class PlayerDataScriptable : ScriptableObject
{
    public string PlayerName;
    public int PlayerPosition;
    public int PlayerMoney;
    public bool isTurn;
    public bool[] OwnedRegions;
   

}