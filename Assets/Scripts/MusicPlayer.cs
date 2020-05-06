using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    private AudioSource _audioSource;

    private static MusicPlayer instance = null;
    public static MusicPlayer Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic() {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic() {
        _audioSource.Stop();
    }
}