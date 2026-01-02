using UnityEngine;

public enum ItemType
{
    Med,
    Battery,
    Note,
    Key
}

[CreateAssetMenu(menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public int maxStack = 1;
}