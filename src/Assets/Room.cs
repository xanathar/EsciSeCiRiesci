using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Room
{
    public static IEnumerable<Room> RoomLogicFactory()
    {
        yield return new Soggiorno();
    }




    private RoomManager roomManager;

    public abstract RoomType GetRoomType();
    public abstract IEnumerable<EntityType> AcceptedEntities();
    public abstract void StartInteraction(IEntity e);
    public abstract void StartInventoryInteraction(EntityType inventory, IEntity e);
    public abstract void ConfirmInteraction(IEntity e);
    public abstract bool ConfirmInventoryInteraction(EntityType inventory, IEntity e);

    public virtual void StopInteraction(IEntity e)
    {
        ClearPrompt();
    }

    public void Init(RoomManager roomManager)
    {
        this.roomManager = roomManager;
    }

    protected void Log(string format, params object[] args)
    {
        this.roomManager.ActionLog.Log(string.Format(format, args));
    }

    protected void LogRandomFailure()
    {
        this.Log("Non ha molto senso.");
    }

    protected void Prompt(string format, params object[] args)
    {
        this.roomManager.InteractionText.text = string.Format(format, args);
    }

    protected void ClearPrompt()
    {
        this.roomManager.InteractionText.text = this.roomManager.Inventory.GetDefaultActionInteractionText();
    }

    protected InventoryManager Inventory
    {
        get { return this.roomManager.Inventory; }
    }

    protected void PickEntity(IEntity e)
    {
        Sprite S = e.Pick();
        Inventory.AddItem(S, e.GetEntityType());
        GameState.PickedEntities.Add(e.GetEntityType());
        ClearPrompt();
    }

    protected void Travel(RoomType rt)
    {
        GameState.CurrentRoom = rt;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Inventory.Backup();
    }

}

