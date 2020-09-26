using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayQueuePlayable : PlayableBehaviour
{
    private int currentClipIndex = -1;
    private float timeToNextClip;
    private Playable mixer;

    public void Init(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph)
    {
        owner.SetInputCount(1);
        mixer = AnimationMixerPlayable.Create(graph, clipsToPlay.Length);
        graph.Connect(mixer, 0, owner, 0);
        owner.SetInputWeight(0, 1);
        for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); clipIndex++)
        {
            graph.Connect(AnimationClipPlayable.Create(graph, clipsToPlay[clipIndex]), 0, mixer, clipIndex);
            mixer.SetInputWeight(clipIndex, 1f);
        }

    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (mixer.GetInputCount() == 0)
        {
            return;
        }
        timeToNextClip -= info.deltaTime;
        if (timeToNextClip <= 0f)
        {
            currentClipIndex++;
            if (currentClipIndex >= mixer.GetInputCount())
            {
                currentClipIndex = 0;
            }
            var currentClip = (AnimationClipPlayable)mixer.GetInput(currentClipIndex);
            currentClip.SetTime(0);
            // 一个一个播动画
            timeToNextClip = currentClip.GetAnimationClip().length;
        }
        for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); clipIndex++)
        {
            if (clipIndex == currentClipIndex)
            {
                mixer.SetInputWeight(clipIndex, 1f);
            }
            else
            {
                mixer.SetInputWeight(clipIndex, 0);
            }
        }

    }




}
