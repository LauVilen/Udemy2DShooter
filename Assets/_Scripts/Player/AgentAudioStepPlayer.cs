using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudioStepPlayer : AudioPlayer
{

    [SerializeField]
    protected AudioClip stepClip;

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(stepClip);
    }
}
