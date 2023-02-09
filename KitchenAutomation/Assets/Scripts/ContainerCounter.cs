using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabedObject;
    [SerializeField] private KitchenItemSO kitchenItemSO;
    
    public override void Interact(Player player) {
        if (!player.HasKitchenItem()) {
            KitchenItem.SpawnKitchenItem(kitchenItemSO, player);

            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
        } else {
            Debug.Log("Player already has something in hand!");
        }
    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("Cannot cut here!");
    }
}
