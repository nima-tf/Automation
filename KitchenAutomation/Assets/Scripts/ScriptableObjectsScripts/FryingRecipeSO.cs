using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject {
    public KitchenItemSO input;
    public KitchenItemSO output;
    public float fryingTimeMax;
}
