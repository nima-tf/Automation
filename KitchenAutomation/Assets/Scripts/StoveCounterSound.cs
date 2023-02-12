using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged; 
    }

    private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool sizzling = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (sizzling) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}
