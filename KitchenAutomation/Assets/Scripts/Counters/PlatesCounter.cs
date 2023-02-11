using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenItemSO plateKitchenItemSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 5f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 5;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenItem()) {
            // player hand is empty
            if (platesSpawnedAmount > 0) {
                // there is atleast one plate spawned
                platesSpawnedAmount--;
                KitchenItem.SpawnKitchenItem(plateKitchenItemSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
        
    }
}
