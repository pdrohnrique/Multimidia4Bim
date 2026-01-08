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
    
    [TextArea(3, 10)]
    public string noteText; // usado apenas se este item for uma nota
}