using System;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material originalMaterial;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration = 0.4f;
    private float flashTimeElapsed = Mathf.Infinity;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private void Update()
    {
        flashTimeElapsed += Time.deltaTime;

        if (flashTimeElapsed > flashDuration && sr.material != originalMaterial)
        {
            sr.material = originalMaterial;
        }
    }

    public void Activate()
    {
        print("activated");
        flashTimeElapsed = 0;
        sr.material = flashMaterial;
    }


}
