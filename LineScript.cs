using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LineScript : MonoBehaviour
{
    public GameObject Line;
    private LineRenderer[] line_renderers;
    public int points = 512;
    private int line_number = 1;
    private int segments;

    public int wave_freq = 15;
    public float wave_amp = 0.2f;
    public float wave_radius = 2f;

    private float[] wave_phases;
    // Start is called before the first frame update
    void Start()
    {
        line_renderers = new LineRenderer[line_number];
        for (int i = 0; i < line_number; i++)
        {
            GameObject a_line = (GameObject)Instantiate(Line);
            a_line.name = "Line " + i.ToString();
            line_renderers[i] = a_line.GetComponent<LineRenderer>();
        }
        segments = AudioGetter.segments;
        wave_phases = new float[segments];
        for (int i = 0; i < segments; i++)
        {
            wave_phases[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] line_vectors = new Vector3[points];

        for (int i = 0; i < points; i++)
        {
            float wave = wave_radius;
            for (int j = 0; j < segments; j++)
            {
                wave += wave_amp * Mathf.Sin((2 * j + 1) * wave_freq * Mathf.PI / points * 2 * i - wave_phases[j]) * Mathf.Clamp(AudioGetter.modified_spectrum[j] * 100, 0f, 1f);
                wave_phases[j] += AudioGetter.modified_spectrum[j] / 1000f;
            }
            float x = wave * Mathf.Cos(Mathf.PI / points * 2 * i);
            float y = wave * Mathf.Sin(Mathf.PI / points * 2 * i);
            line_vectors[i] = new Vector3(x, y, 0);
        }

        for (int i = 0; i < line_number; i++)
        {
            line_renderers[i].positionCount = points;
            line_renderers[i].SetPositions(line_vectors);
            line_renderers[i].loop = true;
        }

        for (int j = 0; j < segments; j++)
        {
            //Float will stop increasing after 256 due to the rounding and storage limitations that occur with floats
            if (wave_phases[j] == 256)
            {
                wave_phases[j] = 0f;
            }
        }
    }
}
