using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ComputerPassword: Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Computer_Esci;
    }

    public override void ConfirmInteraction(EntityType e)
    {
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        return false;
    }

    public override void EnterRoom()
    {
    }

    public override RoomType GetRoomType()
    {
        return RoomType.Computer_Password;
    }

    public override void StartInteraction(EntityType e)
    {
    }

    public override void StartInventoryInteraction(EntityType inventory, EntityType e)
    {
    }
}
