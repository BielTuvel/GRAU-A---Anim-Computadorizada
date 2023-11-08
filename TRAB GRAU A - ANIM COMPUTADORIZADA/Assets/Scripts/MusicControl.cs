using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MusicControl : MonoBehaviour
{
    public AudioSource audioSource; //AudioSource com as músicas
    public Button button1;
    public Button button2;
    public Button button3;

    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;

    [SerializeField]
    private VideoPlayer _musicClip;

    private bool isMusicPlaying = false;

    void Start()
    {
        audioSource.clip = music1; //Atribui a música original de início
        audioSource.Play();
        _musicClip.Play(); //Play no vídeo também
        button1.onClick.AddListener(() => PlayMusic(music1));
        button2.onClick.AddListener(() => PlayMusic(music2));
        button3.onClick.AddListener(() => PlayMusic(music3));
    }

    void PlayMusic(AudioClip music) //Função para alterar a versão da música ORIGINAL, LOW e MEDIUM
    {
        _musicClip.Stop();
        if (isMusicPlaying)
        {
            audioSource.Stop();
            isMusicPlaying = false;
        }

        audioSource.clip = music;
        audioSource.Play();
        _musicClip.Play();
        isMusicPlaying = true;
    }
}
