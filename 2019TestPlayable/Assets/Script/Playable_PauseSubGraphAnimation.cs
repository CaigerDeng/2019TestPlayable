using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class Playable_PauseSubGraphAnimation : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;
    private PlayableGraph graph;
    private AnimationLayerMixerPlayable mixer;
    private AnimationClipPlayable clipPlay0;
    private AnimationClipPlayable clipPlay1;

    void Start()
    {
        graph = PlayableGraph.Create("PauseSubGraphAnimation");
        AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        mixer = AnimationLayerMixerPlayable.Create(graph, 2);
        output.SetSourcePlayable(mixer);
        clipPlay0 = AnimationClipPlayable.Create(graph, clip0);
        clipPlay1 = AnimationClipPlayable.Create(graph, clip1);
        graph.Connect(clipPlay0, 0, mixer, 0);
        graph.Connect(clipPlay1, 0, mixer, 1);
        mixer.SetInputWeight(0, 1f);
        mixer.SetInputWeight(1, 1f);
       

        graph.Play();

    }


    void OnDisable()
    {
        graph.Destroy();

    }

    public void ClickPauseClip1()
    {
        clipPlay1.Pause();

    }



}
