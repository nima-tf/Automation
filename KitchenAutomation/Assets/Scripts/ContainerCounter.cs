using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabedObject;
    [SerializeField] private KitchenItemSO kitchenItemSO;
    
    public override void Interact(Player player) {
        if (!player.HasKitchenItem()) {
            Transform kitchenItemTransform = Instantiate(this.kitchenItemSO.prefab);
            kitchenItemTransform.GetComponent<KitchenItem>().SetKitchenItemParent(player);
            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
        } else {
            Debug.Log("Player already has something in hand!");
        }
    }
}
