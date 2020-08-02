using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public float appearTime;
    public float stayTime;
    public float disappearTime;

    public event GameFlowManager.GameplayEvent OnSplashScreenDone;
    public Image splashImage;

    public void StartSplashScreen()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        float remainingTime = appearTime;
        splashImage.gameObject.SetActive(true);
        Color fadeAmmount = splashImage.material.color;
        while (remainingTime >= 0)
        {
            remainingTime -= Time.deltaTime;
            fadeAmmount.a = 1f - remainingTime/appearTime;
            splashImage.material.color = fadeAmmount;
            yield return null;
        }

        remainingTime = 0f;
        while (remainingTime <= stayTime)
        {
            remainingTime += Time.deltaTime;
            yield return null;
        }

        remainingTime = disappearTime;
        fadeAmmount = splashImage.material.color;
        while (remainingTime >= 0f)
        {
            remainingTime -= Time.deltaTime;
            fadeAmmount.a = remainingTime / disappearTime;
            splashImage.material.color = fadeAmmount;
            yield return null;
        }
        splashImage.gameObject.SetActive(false);
        splashImage.material.color = Color.white;
        if(OnSplashScreenDone != null)
            OnSplashScreenDone.Invoke();
    }
}
