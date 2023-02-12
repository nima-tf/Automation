using System;
using UnityEngine;

public class TrashCounter : BaseCounter {
    public static event EventHandler OnAnyItemTrashed;
    new public static void ResetStaticDate() {
        OnAnyItemTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenItem()) {
            player.GetKitchenItem().DestroySelf();

            OnAnyItemTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
