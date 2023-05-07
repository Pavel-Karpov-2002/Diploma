using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator), typeof(SpriteRenderer))]
public abstract class NPC : Unit
{
    [SerializeField] protected DialogParameters dialogParameters;
    [SerializeField] protected NPCParameters npcParameters;
    [SerializeField] protected SpriteRenderer npcSkin;
    [SerializeField] protected Animator npcAnimator;

    private bool isExpectation;

    public bool IsExpectation { get => isExpectation; protected set => isExpectation = value; }

    public delegate void UpdateQuestions();

    private void Awake()
    {
        isExpectation = true;
    }

    protected virtual void NewQuestions()
    {
        DialogPanelSingleton.GetInstance().ShowNewQuestion();
    }

    public abstract void SetExpectation(bool expectation);
    protected abstract void SetSkin();
}
