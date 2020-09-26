using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;


/// <summary>
/// 这篇程序可以参考官方 https://docs.unity3d.com/ScriptReference/Playables.PlayableGraph.Connect.html
/// </summary>
[RequireComponent(typeof(Animator))]
public class Playable_RotateMoveCube : MonoBehaviour
{
    public AnimationClip animClipA;
    public AnimationClip animClipB;

    private PlayableGraph graph;
    private AnimationLayerMixerPlayable mixer;
    private AnimationPlayableOutput output;


    void Start()
    {
        graph = PlayableGraph.Create("RotateMoveCube");
        output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        // inputCount 可以接几个playable
        mixer = AnimationLayerMixerPlayable.Create(graph, 3);
        output.SetSourcePlayable(mixer);

        AnimationClipPlayable clipPlayA = AnimationClipPlayable.Create(graph, animClipA);
        AnimationClipPlayable clipPlayB = AnimationClipPlayable.Create(graph, animClipB);
        // sourceOutputPort 默认为0
        graph.Connect(clipPlayA, 0, mixer, 0);
        graph.Connect(clipPlayB, 0, mixer, 1);
        // weight填0.5f只会有一半效果，和我理解的a + b + c = 1不同
        mixer.SetInputWeight(0, 1f);
        mixer.SetInputWeight(1, 1f);
        graph.Play();

    }

    public void ClickClose()
    {
        // ...这里暂无判空等检测
        graph.Disconnect(mixer, 0);
        graph.Disconnect(mixer, 1);

        graph.DestroyPlayable(mixer);
        graph.DestroyOutput(output);

        // cube位置会停在销毁的时刻

    }

    public void ClickStopB()
    {
        // ...这里暂无判空等检测
        mixer.SetInputWeight(1, 0f);

        //graph.Disconnect(mixer, 1);

    }

    void OnDisable()
    {
        graph.Destroy();

    }

}
