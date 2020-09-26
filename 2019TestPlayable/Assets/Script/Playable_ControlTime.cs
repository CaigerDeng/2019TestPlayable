using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class Playable_ControlTime : MonoBehaviour
{
    public AnimationClip clip;
    public float time;

    private PlayableGraph graph;
    private AnimationClipPlayable clipP;


    void Start()
    {
        graph = PlayableGraph.Create("ControlTime");
        var output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        clipP = AnimationClipPlayable.Create(graph, clip);
        output.SetSourcePlayable(clipP);
        graph.Play();
        clipP.Pause();


    }

    void Update()
    {
        clipP.SetTime(time);

    }

    void OnDisable()
    {
        graph.Destroy();

    }

}
