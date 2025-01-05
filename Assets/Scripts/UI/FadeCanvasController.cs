using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvasController : MonoBehaviour
{
    public Image fadeImage;
    public VoidEventSO fadeEventSo;
    
    private void OnEnable()
    {
        fadeEventSo.OnEventRaised += FadeMonth;
    }

    private void OnDisable()
    {
        fadeEventSo.OnEventRaised -= FadeMonth;
    }

    private void FadeMonth()
    {
        //图片可见则衰退
        bool isFade = fadeImage.gameObject.activeInHierarchy;
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(SetImageColor_A(isFade));
    }

    /// <summary>
    /// 颜色衰退
    /// </summary>
    /// <param name="isFade"></param>
    /// <returns></returns>
    private IEnumerator SetImageColor_A(bool isFade)
    {
        int targetInt = isFade ? 0 : 1;
        var currenta = fadeImage.color;
        while (Math.Abs(currenta.a - targetInt) > 0.015f)
        {
            currenta.a += isFade ? -0.01f : 0.02f;
            fadeImage.color = currenta;
            yield return null;
        }

        currenta.a = targetInt;
        fadeImage.color = currenta;
        
        if (isFade)
        {
            fadeImage.gameObject.SetActive(false);
        }
        
    }
}