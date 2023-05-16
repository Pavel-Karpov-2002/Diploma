using UnityEngine;

[CreateAssetMenu(fileName = "AudioParameters", menuName = "CustomParameters/AudioParameters")]
public class AudioParameters : ScriptableObject
{
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip movement;
    [SerializeField] private AudioClip openWardborde;
    [SerializeField] private AudioClip openingInventory;
    [SerializeField] private AudioClip openingBackpack;
    [SerializeField] private AudioClip changeSkin;
    [SerializeField] private AudioClip pageRustling;
    [SerializeField] private AudioClip warning;
    [SerializeField] private AudioClip itemUsed;
    [SerializeField] private AudioClip levelPassed;
    [SerializeField] private AudioClip levelNotPassed;

    public AudioClip DoorOpen => doorOpen;
    public AudioClip Movement => movement;
    public AudioClip OpenWardborde => openWardborde;
    public AudioClip OpeningInventory => openingInventory;
    public AudioClip OpeningBackpack => openingBackpack;
    public AudioClip ChangeSkin => changeSkin;
    public AudioClip PageRustling => pageRustling;
    public AudioClip Warning => warning;
    public AudioClip ItemUsed => itemUsed;
    public AudioClip LevelPassed => levelPassed;
    public AudioClip LevelNotPassed => levelNotPassed;
}
