using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public int sampleIndex = 0;
    private Renderer musicRenderer;
    private AudioSource audioSource;
    private float[] samples = new float[64];
    private float threshold = .2f;
    private float refillRate = 0.01f; 

    public float viewLevel; 

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        musicRenderer = GetComponent<Renderer>();
	}
    float value;
    
    // Update is called once per frame
    void Update () {
        audioSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
        float level = (Mathf.Clamp(samples[sampleIndex] * (1 + sampleIndex * sampleIndex), 0, 1));
        viewLevel = level;
        if (level > threshold) {
            value = 1 - level;
        } else if (value <= 1f) {
            value += refillRate;
        }
        musicRenderer.material.SetFloat("_Transparency", value);
    }
}
