using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int medsCount;
    public int batteryCount;
    public int notesCount;
    public int keysCount;

    public void AddItem(ItemData item)
    {
        switch (item.type)
        {
            case ItemType.Med:
                medsCount++;
                break;
            case ItemType.Battery:
                batteryCount++;
                break;
            case ItemType.Note:
                notesCount++;
                // aqui você também pode registrar no NotesManager
                break;
            case ItemType.Key:
                keysCount++;
                // se quiser, também pode setar hasKey no PlayerController
                break;
        }
    }

    public bool UseMed()
    {
        if (medsCount <= 0) return false;
        medsCount--;
        return true;
    }

    public bool UseBattery()
    {
        if (batteryCount <= 0) return false;
        batteryCount--;
        return true;
    }

    public bool UseKey()
    {
        if (keysCount <= 0)
            return false;
        
        keysCount--;
        return true;
    }
}