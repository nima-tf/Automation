using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour {
    private void Awake() {
        // remove static data of the objects from previous gameScenes
        BaseCounter.ResetStaticDate();
        CuttingCounter.ResetStaticDate();
        TrashCounter.ResetStaticDate();
    }
}
