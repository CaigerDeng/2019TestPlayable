using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


[RequireComponent(typeof(Animator))]
public class Playable_BlendController : MonoBehaviour
{
    public AnimationClip clip;
    public RuntimeAnimatorController controller;
    public float weight;
    private PlayableGraph graph;
    private AnimationMixerPlayable mixer;

    void Start()
    {
        graph = PlayableGraph.Create("BlendController");
        AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());
        mixer = AnimationMixerPlayable.Create(graph, 2);
        output.SetSourcePlayable(mixer);
        AnimationClipPlayable clipP = AnimationClipPlayable.Create(graph, clip);
        AnimatorControllerPlayable ctrlP = AnimatorControllerPlayable.Create(graph, controller);
        graph.Connect(clipP, 0, mixer, 0);
        graph.Connect(ctrlP, 0, mixer, 1);
        graph.Play();

    }

    void Update()
    {
        weight = Mathf.Clamp01(weight);
        mixer.SetInputWeight(0, 1.0f - weight);
        mixer.SetInputWeight(1, weight);

    }

    void OnDisable()
    {
        graph.Destroy();

    }

}
