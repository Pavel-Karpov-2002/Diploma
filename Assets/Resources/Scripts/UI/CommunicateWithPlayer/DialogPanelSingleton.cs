using UnityEngine;

public class DialogPanelSingleton : MonoBehaviour
{
    [SerializeField] private TestingAnswersScript testing;
    [SerializeField] private EnteringResponseScript enteringResponse;

    private static DialogPanelSingleton instance;

    public delegate void ShowQuestionDelegate();
    public static ShowQuestionDelegate ShowQuestionDialog;

    public NPCQuestions NpcQuestions { get; set; }
    
    public TestingAnswersScript Testing => testing;
    public EnteringResponseScript EnteringResponse => enteringResponse;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowNewQuestion()
    {
        ShowQuestionDialog?.Invoke();
    }

    public static DialogPanelSingleton GetInstance()
    {
        /*if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<DialogPanelSingleton>()[0];
*/
        return instance;
    }

    private void OnDestroy()
    {
        ShowQuestionDialog = null;
        instance = null;
    }
}
