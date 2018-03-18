﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpell : MonoBehaviour {

    AudioSource playSpell;

    public AudioClip kamehameha;
    public AudioClip iceSlow;
     AudioClip bulletAk;

    Tiro tiro;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playSpell = GetComponent<AudioSource>();
        tiro = player.GetComponent<Tiro>();
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayAudio();

    }

    void PlayAudio()
    {
        if (tiro.playKame == true)
        {
            playSpell.clip = kamehameha;
            playSpell.Play();
        }
        if (tiro.playAk == true)
        {
            playSpell.clip = bulletAk;
            playSpell.Play();
        }
        if (tiro.playIce == true)
        {
            playSpell.clip = iceSlow;
            playSpell.Play();
        }

    }

}
