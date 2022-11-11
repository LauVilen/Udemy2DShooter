using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLightFeedback : Feedback
{
    [SerializeField] private Light2D lightTarget = null;
    [SerializeField] private float LightOnDelay = 0.01f, lightOffDelay = 0.01f;
    [SerializeField] private bool defaultState = false;
    public override void CreateFeedback()
    {
        StartCoroutine(ToggleLightCoroutine(LightOnDelay, true,
            () => StartCoroutine(ToggleLightCoroutine(lightOffDelay, false))));
    }

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        lightTarget.enabled = defaultState;
    }

    IEnumerator ToggleLightCoroutine(float time, bool result, Action finishCallBack = null)
    {
        yield return new WaitForSeconds(time);
        lightTarget.enabled = result;
        finishCallBack?.Invoke();
    }
}
