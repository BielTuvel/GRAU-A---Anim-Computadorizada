using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPulse : MonoBehaviour
{
    public float baseScale = 1.0f;
    public float pulseMultiplier = 2.0f;  //Fator de multiplicação para a escala
    public float minFrequency = 20f;  //Frequência mínima
    public float maxFrequency = 20000f;  //Frequência máxima

    private AudioSource audioSource;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>(); //Audio Source
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
                average += spectrumData[i]; //Coletando a frequência desejada entre o range para acentuá-la
            }
        }

        //Calculando a escala com a base + frequência coletada * o multiplicador (deixei 0.5f no projeto, 2.0f fica legal mas meio exagerado, perde a beleza haha)
        float pulseScale = baseScale + average * pulseMultiplier;

        //Aplica o efeito de "pulsação" no objeto
        transform.localScale = initialScale * pulseScale;
    }
}
