using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineVisualizer : MonoBehaviour {

    public int sampleIndex = 0;
    private Renderer musicRenderer;
    private AudioSource audioSource;
    private float[] samples = new float[64];

    public float value;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        musicRenderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        audioSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
        musicRenderer.material.SetFloatArray("_SampleValues", samples);

        //value = musicRenderer.material.GetFloat("_Vertex");
    }
}
