using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const float MUSIC_VOLUME_DEFAULT = .3f;

    private AudioSource audioSource;
    private float volume;

    private void Awake() {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        // default setting
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, MUSIC_VOLUME_DEFAULT);
        audioSource.volume = volume;
    }

    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1f) {
            volume = 0f;
        }
        audioSource.volume = volume;

        // save option setting prefs
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
