using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRegionDataObj", menuName = "myObjects/RegionData")]
public class RegionDataScriptable : ScriptableObject
{
    public bool HasOwner;
    public string MuseumName;
    public int ByingPrice;
    public int EntrancePrice;
    public int[] ExhibitPrices;
    public int[] TicketPrices;
    public int BuiltExhibits;
    public GameObject[] Buildings;
    public RegionType type;
}
public enum RegionType
{
    Outer, Inner
}
