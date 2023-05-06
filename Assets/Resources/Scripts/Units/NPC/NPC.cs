using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class NPC : Unit
{
    [SerializeField] protected DialogParameters dialogParameters;
    [SerializeField] protected NPCParameters nPCParameters;

    private bool isExpectation;

    public bool IsExpectation { get => isExpectation; protected set => isExpectation = value; }

    public delegate void UpdateQuestions();

    private void Awake()
    {
        isExpectation = true;
    }

    public abstract void SetExpectation(bool expectation);

    protected virtual void NewQuestions()
    {
        DialogPanelSingleton.GetInstance().ShowNewQuestion();
    }

    private void SetSkin()
    {
        // рандомная генерация модельки
    }
}
