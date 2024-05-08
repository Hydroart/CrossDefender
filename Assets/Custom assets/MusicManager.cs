using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip MenuSong;
    public AudioClip GameSong;
    private AudioSource audioSource;
    private string lastScene;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastScene = SceneManager.GetActiveScene().name;

        // Automatically play music based on the initial scene
        ChangeSong();
    }

    void Update()
    {
        // Check if the scene has changed and update lastScene accordingly
        if (SceneManager.GetActiveScene().name != lastScene)
        {
            lastScene = SceneManager.GetActiveScene().name;
            ChangeSong();
        }
    }

    public static class MusicState
{
    private static int musicState = 1; // 1 for music on, 0 for music off

    public static int GetMusicState()
    {
        return musicState;
    }

    public static void SetMusicState(int state)
    {
        musicState = state;
    }
}


    // Changing the song when loading a specific level
    void ChangeSong()
    {
        if (MusicState.GetMusicState() == 1)
        {
            if (lastScene == "Menu")
            {
                audioSource.PlayOneShot(MenuSong, 0.4f);
                Debug.Log("Var lastScene is now: " + lastScene);
            }
            else if (lastScene == "Demo")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(GameSong, 0.7f);
                Debug.Log("Var lastScene is now: " + lastScene);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void PlayMusic(bool play)
    {
        if (play)
        {
            if (!audioSource.isPlaying)
            {
                ChangeSong();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
