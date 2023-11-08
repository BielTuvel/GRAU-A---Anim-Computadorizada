using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioColor : MonoBehaviour
{
    public Color baseColor = Color.white;
    public float colorIntensity = 1.0f;
    public float minFrequency = 20f;  // Frequência mínima
    public float maxFrequency = 20000f;  // Frequência máxima

    private AudioSource audioSource;
    private Material material;
    private Color _initialColor;

    void Start()
    {
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();  //Pegando o audio source com a música
        material = GetComponent<Renderer>().material; //Pegando o material do objeto
        _initialColor = material.color;
    }

    void Update()
    {
        float[] spectrumData = new float[256];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Triangle);

        float average = 0f;

        for (int i = 0; i < spectrumData.Length; i++)
        {
            float frequency = i * audioSource.clip.frequency / 2 / spectrumData.Length;
            if (frequency >= minFrequency && frequency <= maxFrequency)
            {
                average += spectrumData[i]; //Coletando a frequência dentro do range
            }
        }

        //Mudando a intensidade baseado na cor base e na frequência
        Color newColor = baseColor + new Color(average, average, average) * colorIntensity;

        material.color = newColor;
    }
}
