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

    void Start()
    {
        script1 = FindObjectOfType<BasicEnemyController>();
        script2 = FindObjectOfType<BasicEnemyController2>();
        script3 = FindObjectOfType<EnemyProjectileScript>();

        foreach (Image img in images)
        {
            img.color = originalColor;  
        }

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
}


