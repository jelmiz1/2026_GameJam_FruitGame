using System.Runtime.CompilerServices;
using UnityEngine;


public class Billboarding : MonoBehaviour 
{
    [SerializeField] Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        transform.LookAt(camera.transform);
    }
}
