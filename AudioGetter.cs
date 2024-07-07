using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGetter : MonoBehaviour
{
    AudioSource audiosource;
    public static int segments = 16;
    public static float[] spectrum = new float[512];
    public static float[] modified_spectrum;
    public float[] show = spectrum;
    public float[] show_mod = modified_spectrum;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        modified_spectrum = new float[segments];
    }

    // Update is called once per frame
    void Update()
    {
        audiosource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        for (int i = 0; i < segments; i++)
        {
            for (int j = 512 / segments * i; j < (i+1) * 512 / segments; j++)
            {
                modified_spectrum[i] += spectrum[j];
            }
            modified_spectrum[i] /= 128;
        }
        show = spectrum;
        show_mod = modified_spectrum;
    }
}
