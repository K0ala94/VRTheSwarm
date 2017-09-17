using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarkerController : MonoBehaviour {

    private bool fading;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fading = true;
        }
    }

    private IEnumerator fadeAway()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        for (float opacity = ps.main.startColor.color.a; opacity > 0.007; opacity -= 0.007f)
        {
            ParticleSystem.MainModule main = ps.main;
            Color c = ps.main.startColor.color;
            c.a = opacity;
            main.startColor = new ParticleSystem.MinMaxGradient(c);
    
            yield return null;
        }

        DestroyImmediate(gameObject);
        

    }

    private void Update()
    {
        if (fading)
        {
            StartCoroutine( fadeAway());
        }
    }
}
