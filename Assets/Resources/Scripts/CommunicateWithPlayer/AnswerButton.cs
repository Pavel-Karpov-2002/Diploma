using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private Image buttonImage;

    private bool valid;
    
    public bool Valid
    {
        get { return valid; }
        set { valid = value; }
    }

    public void ClickToAnswer()
    {
        if (valid)
        {
            ChangeButtonColor(gameParameters.Dialog.ButtonTrueColor);
        }
        else
        {
            ChangeButtonColor(gameParameters.Dialog.ButtonFalseColor);
        }
    }

    private void ChangeButtonColor(Color color)
    {
        buttonImage.color = color;
    }
}
