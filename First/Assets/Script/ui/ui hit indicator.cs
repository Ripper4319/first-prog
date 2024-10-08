using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeBriefly : MonoBehaviour
{
   
    [Header("detectable hit types")]
    public BasicEnemyController script1;
    public BasicEnemyController2 script2;
    public EnemyProjectileScript script3;

    public Image[] images;  
    public Color targetColor = Color.red;  
    public Color originalColor = Color.white;
    public float colorChangeDuration = .3f;
    private bool isColorChanging = false;

    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        script1 = FindObjectOfType<BasicEnemyController>();
        script2 = FindObjectOfType<BasicEnemyController2>();
        script3 = FindObjectOfType<EnemyProjectileScript>();

        foreach (Image img in images)
        {
            img.color = originalColor;  
        }

        StartCoroutine(FadeIn());

    }
    private void Update()
    {
        if(script1 != null && script1.hit)
        {
            StartCoroutine(ChangeColors());
        }
        if (script2 != null && script2.hit)
        {
            StartCoroutine(ChangeColors());
        }
        if (script3 != null && script3.hit)
        {
            StartCoroutine(ChangeColors());
        }
    }
    private IEnumerator ChangeColors()
    {
        isColorChanging = true;

        foreach (Image img in images)
        {
            img.color = targetColor;  
        }

        yield return new WaitForSeconds(colorChangeDuration); 

        foreach (Image img in images)
        {
            img.color = originalColor;  
        }

        isColorChanging = false;
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public void TriggerFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}


