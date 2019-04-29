using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Space)); }
    private bool _right() { return (Input.GetKeyDown(KeyCode.RightArrow)); }
    private bool isStory;
    public AudioSource storyMusic;
    public AudioSource select;

    private Vector3 cardPos;
    private GameObject card2;
    private GameObject card3;
    private GameObject card4;
    private GameObject card5;
    private GameObject card6;
    private GameObject card7;
    private GameObject card8;
    private GameObject card9;
    private GameObject card10;
    private GameObject card11;

    private float selectVolume;

    private Scene _activeScene;

    // Use this for initialization
    void Awake () {

        cardPos = new Vector3(0, 0, 0);
        card2 = GameObject.Find("Card2");
        card3 = GameObject.Find("Card3");
        card4 = GameObject.Find("Card4");
        card5 = GameObject.Find("Card5");
        card6 = GameObject.Find("Card6");
        card7 = GameObject.Find("Card7");
        card8 = GameObject.Find("Card8");
        card9 = GameObject.Find("Card9");
        card10 = GameObject.Find("Card10");
        card11 = GameObject.Find("Card11");

        isStory = true;
        InitAudio();
        _activeScene = SceneManager.GetActiveScene();

    }
	
	// Update is called once per frame
	void Update ()
    {
        CardSelect();

        if ((_select()) && (isStory))
        {
            if (_activeScene.name == "Story1")
            {
                StartCoroutine(LoadLevel());
            }

        }
        
    }

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        storyMusic = audio[0];
        select = audio[1];
        select.playOnAwake = false;
        storyMusic.volume = .9f;
        select.volume = .4f;
        storyMusic.Play();
        storyMusic.loop = true;
    }

    private void CardSelect()
    {
        if ((_right()) && (isStory))
        {
            if (card2.transform.position.x != cardPos.x)
            {
                card2.transform.position = cardPos;
            }
            else if (card3.transform.position.x != cardPos.x)
            {
                card3.transform.position = cardPos;
            }
            else if (card4.transform.position.x != cardPos.x)
            {
                card4.transform.position = cardPos;
            }
            else if (card5.transform.position.x != cardPos.x)
            {
                card5.transform.position = cardPos;
            }
            else if (card6.transform.position.x != cardPos.x)
            {
                card6.transform.position = cardPos;
            }
            else if (card7.transform.position.x != cardPos.x)
            {
                card7.transform.position = cardPos;
            }
            else if (card8.transform.position.x != cardPos.x)
            {
                card8.transform.position = cardPos;
            }
            else if (card9.transform.position.x != cardPos.x)
            {
                card9.transform.position = cardPos;
            }
            else if (card10.transform.position.x != cardPos.x)
            {
                card10.transform.position = cardPos;
            }
            else if (card11.transform.position.x != cardPos.x)
            {
                card11.transform.position = cardPos;
            }
        }
    }

    IEnumerator LoadLevel()
    {
        isStory = false;
        select.Play();
        storyMusic.Stop();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Jtest");
    }
}
