using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GameState
{
    public static RoomType CurrentRoom = RoomType.Cucina;
    public static HashSet<EntityType> PickedEntities = new HashSet<EntityType>();
    public static List<EntityType> InventoryItems = new List<EntityType>();

    public static void Dump()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Current room = " + CurrentRoom.ToString());

        sb.AppendLine("Picked entities:");

        foreach (var ee in PickedEntities)
            sb.AppendLine("\t" + ee.ToString());

        sb.AppendLine("Inventory items:");

        foreach (var ee in InventoryItems)
            sb.AppendLine("\t" + ee.ToString());

        Debug.Log(sb.ToString());
    }
}
