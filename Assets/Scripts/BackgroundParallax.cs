using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform[] _backgroundLayers;
    [SerializeField] private float[] _coefficient;

    private int _layersCount;

    private void Start() { _layersCount = _backgroundLayers.Length; }

    private void Update()
    {
        for(int i = 0; i < _layersCount; i++)
        {
            _backgroundLayers[i].position = transform.position * _coefficient[i]; 
        }
    }
}
