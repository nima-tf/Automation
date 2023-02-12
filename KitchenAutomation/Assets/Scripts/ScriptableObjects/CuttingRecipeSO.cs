using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject {
    public KitchenItemSO input;
    public KitchenItemSO output;
    public int cuttingProgressMax;
}
