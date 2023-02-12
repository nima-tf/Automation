using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject {
    public KitchenItemSO input;
    public KitchenItemSO output;
    public float burningTimeMax;
}
