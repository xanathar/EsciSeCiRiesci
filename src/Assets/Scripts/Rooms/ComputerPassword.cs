using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ComputerPassword: Room
{
    public override IEnumerable<EntityType> AcceptedEntities()
    {
        yield return EntityType.Computer_Esci;
        yield return EntityType.Computer_PasswordLock;
    }

    public override void ConfirmInteraction(EntityType e)
    {
        if (e == EntityType.Computer_Esci)
        {
            Travel(RoomType.Camera);
            return;
        }

        LogRoom("lock_password");
    }

    public override bool ConfirmInventoryInteraction(EntityType inventory, EntityType e)
    {
        if (inventory == EntityType.Password && e == EntityType.Computer_PasswordLock)
        {
            Travel(RoomType.Computer);
        }
        else
        {
            LogRandomFailure();
        }

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
        if (inventory != EntityType.Password)
        {
            PromptRoom("inventario_no_password");
        }
        else
        {
            if (e == EntityType.Computer_PasswordLock)
            {
                PromptRoom("password_sblocca");
            }
            else
            {
                PromptRoom("password_su_altro");
            }
        }
    }
}
