using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public SplashScreen firstSplash;
    public SplashScreen secondSplash;

    public event GameFlowManager.GameplayEvent OnSplashesDone;

    void Start()
    {
        firstSplash.gameObject.SetActive(true);
        firstSplash.OnSplashScreenDone += StartSecondSplash;
        firstSplash.StartSplashScreen();
    }

    void StartSecondSplash()
    {
        secondSplash.gameObject.SetActive(true);
        secondSplash.OnSplashScreenDone += GoToNextLevel;
        secondSplash.StartSplashScreen();
    }

    void GoToNextLevel()
    {
        if(OnSplashesDone != null)
            OnSplashesDone.Invoke();
    }
}
