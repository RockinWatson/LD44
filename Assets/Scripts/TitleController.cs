using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Return)); }
    private bool isTitle;
    public AudioSource titleMusic;
    public AudioSource select;

    private float selectVolume;

    private Scene _activeScene;

    // Use this for initialization
    void Awake () {
        isTitle = true;
        InitAudio();
        _activeScene = SceneManager.GetActiveScene();

	}

    // Update is called once per frame
    void Update() {

        if ((_select()) && (isTitle))
        {
            if (_activeScene.name == "Title")
            {
                StartCoroutine(LoadStory());
            }

        }
        
    }

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        titleMusic = audio[0];
        select = audio[1];
        select.volume = .5f;
        titleMusic.volume = .9f;
        select.playOnAwake = false;
        titleMusic.Play();
        titleMusic.loop = true;
    }

    IEnumerator LoadStory()
    {
        isTitle = false;
        select.Play();
        titleMusic.Stop();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Story1");
    }
}
