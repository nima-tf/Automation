using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenItem()) {
            player.GetKitchenItem().DestroySelf();
        }
    }
}
