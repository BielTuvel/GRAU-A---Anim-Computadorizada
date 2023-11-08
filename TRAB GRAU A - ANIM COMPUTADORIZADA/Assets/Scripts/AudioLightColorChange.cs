using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLightColorChange : MonoBehaviour
{
    public Color[] colors;
    public float colorChangeSpeed = 2.0f;
    public float minFrequency = 20f;  // Frequência mínima do range
    public float maxFrequency = 20000f;  // Frequência máxima
    public float intensityMultiplier = 1.0f;  // Multiplicador da intensidade da luz

    private AudioSource audioSource;
    private Light lightComponent;
    private int currentColorIndex = 0;

    void Start()
    {
        audioSource = GameObject.Find("Audio Manager").GetComponent <AudioSource>();  //Busca o audio source que está com a música
        lightComponent = GetComponent<Light>();
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
                average += spectrumData[i]; //Pega aqui as frequências mais constantes dentro do range
            }
        }

        
        Color nextColor = colors[currentColorIndex];

        //Suavização de cores
        Color lerpedColor = Color.Lerp(lightComponent.color, nextColor, Time.deltaTime * colorChangeSpeed);

        lightComponent.color = lerpedColor;

        //Intensidade da cor multiplicando pelo average das frequências
        float nextIntensity = average * intensityMultiplier;

        lightComponent.intensity = Mathf.Lerp(lightComponent.intensity, nextIntensity, Time.deltaTime * colorChangeSpeed);

        //Troca de cores.
        if (lerpedColor == nextColor)
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }
    }
}