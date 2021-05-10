using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Create Game Settings", order = 1)]
public class GameSettingsDatabase : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject targetPrefab;

    [Header("AudioClips")]
    public AudioClip ballShootSound;
    public AudioClip ballPullSound;
    public AudioClip targetHitSound; 
}
