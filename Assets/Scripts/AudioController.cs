using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource levelMusic;
    public static AudioSource dash; 
    public static AudioSource explosion;
    public static AudioSource pickup; 
    public static AudioSource select; 
    public static AudioSource thunderClap; 
    public static AudioSource vacuum; 
    public static AudioSource zombie;
    public static AudioSource dog1;
    public static AudioSource dog2;
    public static AudioSource dogdie1;
    public static AudioSource dogdie2;

    // Use this for initialization
    void Awake () {
        InitAudio();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        levelMusic = audio[0];
        dash = audio[1];
        explosion = audio[2];
        pickup = audio[3];
        select = audio[4];
        thunderClap = audio[5];
        vacuum = audio[6];
        zombie = audio[7];
        dog1 = audio[8];
        dog2 = audio[9];
        dogdie1 = audio[10];
        dogdie2 = audio[11];

        audio[0].volume = .95f;
        audio[1].volume = .25f;
        audio[2].volume = .5f;
        audio[3].volume = .3f;
        audio[4].volume = .5f;
        audio[5].volume = .5f;
        audio[6].volume = .5f;
        audio[7].volume = .5f;
        audio[8].volume = .5f;
        audio[9].volume = .6f;
        audio[10].volume = .4f;
        audio[11].volume = .4f;

        audio[0].loop = true;
    }
}
