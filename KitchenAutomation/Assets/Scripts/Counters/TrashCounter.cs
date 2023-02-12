using System;
using UnityEngine;

public class TrashCounter : BaseCounter {
    public static event EventHandler OnAnyItemTrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenItem()) {
            player.GetKitchenItem().DestroySelf();

            OnAnyItemTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
