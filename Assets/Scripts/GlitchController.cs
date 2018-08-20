using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchController : MonoBehaviour {

    private Renderer holoRenderer;
    bool doGLitch;
    int cnt = 0;

	// Use this for initialization
	void Start () {
        holoRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        cnt++;
        int rand1 = Random.Range(50, 200);
        if (cnt % rand1 == 0) {
            doGLitch = true;
        } else if (cnt > rand1 + Random.Range(45, 50)) {
            cnt = 0;
            doGLitch = false;
        }

        if (doGLitch) {
            holoRenderer.material.SetFloat("_Amount", 1f);
            holoRenderer.material.SetFloat("_CutoutThresh", .29f);
            holoRenderer.material.SetFloat("_Amplitude", Random.Range(70,100));
            holoRenderer.material.SetFloat("_Speed", Random.Range(0.01f, 0.1f));
        } else {
            holoRenderer.material.SetFloat("_Amount", 0f);
            holoRenderer.material.SetFloat("_CutoutThresh", 0f);
        }
	}
}
