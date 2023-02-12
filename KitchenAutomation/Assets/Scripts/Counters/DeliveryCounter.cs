public class DeliveryCounter : BaseCounter {
    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenItem()) {
            if (player.GetKitchenItem().TryGetPlate(out PlateKitchenItem plateKitchenItem)) {
                // only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenItem);
                player.GetKitchenItem().DestroySelf();
            }
        }
    }

}
