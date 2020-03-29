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
        yield return new Cucina();
        yield return new Corridoio();
        yield return new Computer();
        yield return new ComputerPassword();
    }

    private RoomManager roomManager;

    public abstract RoomType GetRoomType();
    public abstract IEnumerable<EntityType> AcceptedEntities();
    public abstract void StartInteraction(EntityType e);
    public abstract void StartInventoryInteraction(EntityType inventory, EntityType e);
    public abstract void ConfirmInteraction(EntityType e);
    public abstract bool ConfirmInventoryInteraction(EntityType inventory, EntityType e);
    public abstract void EnterRoom();

    public virtual void StopInteraction(EntityType e)
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

    protected void PickEntity(EntityType e)
    {
        Inventory.AddItem(e);
        GameState.AddPickedEntity(e);
        roomManager.GetEntityObject(e).DisableEntityPermanently();
        PlaySound("success");
        ClearPrompt();
        roomManager.Inventory.Backup();
        GameState.Save();
    }

    protected void Travel(RoomType rt)
    {
        GameState.CurrentRoom = rt;
        Inventory.Backup();

        roomManager.StartTravel();
    }

    public Entity GetEntityObject(EntityType entityType)
    {
        return roomManager.GetEntityObject(entityType);
    }

    public RoomOverlay GetRoomOverlay(OverlayType overlayType)
    {
        return roomManager.GetRoomOverlay(overlayType);
    }

    public void StartCoroutine(System.Collections.IEnumerator e)
    {
        roomManager.StartCoroutine(e);
    }

    public void PlaySound(string clip)
    {
        roomManager.SoundFX.Play(clip);
    }
}

