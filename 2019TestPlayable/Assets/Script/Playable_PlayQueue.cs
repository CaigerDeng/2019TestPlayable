using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

/// <summary>
/// 这个例子主要演示怎么写“自定义Playable”代码
/// </summary>
public class Playable_PlayQueue : MonoBehaviour
{

    public AnimationClip[] clipsToPlay;

    private PlayableGraph graph;


    void Start()
    {
        graph = PlayableGraph.Create("PlayQueue");
        var playQueueP = ScriptPlayable<PlayQueuePlayable>.Create(graph);
        var playQueue = playQueueP.GetBehaviour();
        playQueue.Init(clipsToPlay, playQueueP, graph);
        var output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        output.SetSourcePlayable(playQueueP);
        output.SetSourceOutputPort(0);

        graph.Play();

    }

    void OnDisable()
    {
        graph.Destroy();

    }

}
