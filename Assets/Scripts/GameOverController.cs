using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Return)); }
    private bool isGameOver;
    public AudioSource gameOverMusic;
    public AudioSource select;

    private float selectVolume;

    private Scene _activeScene;

    // Use this for initialization
    void Awake () {
        isGameOver = true;
        InitAudio();
        _activeScene = SceneManager.GetActiveScene();

    }
	
	// Update is called once per frame
	void Update () {

        if ((_select()) && (isGameOver))
        {
            if (_activeScene.name == "GameOver")
            {
                StartCoroutine(LoadStory());
            }

        }

    }

    IEnumerator LoadStory()
    {
        isGameOver = false;
        select.Play();
        gameOverMusic.Stop();
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene("Jtest");
    }

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        gameOverMusic = audio[0];
        select = audio[1];
        select.volume = .5f;
        gameOverMusic.volume = .9f;
        select.playOnAwake = false;
        gameOverMusic.Play();
        gameOverMusic.loop = true;
    }


}
