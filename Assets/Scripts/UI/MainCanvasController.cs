using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XTools.UI;

public class MainCanvasController : BasePanel
{
    [Header("窗口参数")] public Image healthFillGreenImage;
    public Image healthFillRedImage;
    public Image powerFillYellowImage;

    /// <summary>
    /// 血量变化
    /// </summary>
    /// <param name="newHealthRatio"></param>
    public void OnHealthChanged(float newHealthRatio)
    {
        healthFillGreenImage.fillAmount = newHealthRatio;
        StartCoroutine(HealthChangedAnimation(newHealthRatio));
    }

    /// <summary>
    /// 血量动画
    /// </summary>
    /// <param name="newHealthratio"></param>
    /// <returns></returns>
    private IEnumerator HealthChangedAnimation(float newHealthratio)
    {
        while (healthFillRedImage.fillAmount > newHealthratio)
        {
            healthFillRedImage.fillAmount -= Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            //yield return null;
        }

        healthFillRedImage.fillAmount = newHealthratio;
    }
}