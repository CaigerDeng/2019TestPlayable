using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


/// <summary>
/// 此脚本可参考官网Manual：https://docs.unity3d.com/Manual/Playables-Examples.html
/// </summary>
[RequireComponent(typeof(Animator))]
public class Playable_RotateCube : MonoBehaviour
{
    public AnimationClip clip;
    private PlayableGraph graph;

    void Start()
    {
        graph = PlayableGraph.Create("RotateCube");
        AnimationPlayableOutput animOutput = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        AnimationClipPlayable clipPlay = AnimationClipPlayable.Create(graph, clip);
        animOutput.SetSourcePlayable(clipPlay);
        graph.Play();

        // 另一种写法，更方便的接口
        //AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out graph);

    }

    void OnDisable()
    {
        graph.Destroy();
    }

}
