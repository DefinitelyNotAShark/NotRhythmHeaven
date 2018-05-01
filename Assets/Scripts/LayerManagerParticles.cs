using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManagerParticles : MonoBehaviour {

    [SerializeField]
    private Renderer particleRenderer;

    private void Start()
    {
        particleRenderer.sortingLayerName = "Particles";
        
    }
}
