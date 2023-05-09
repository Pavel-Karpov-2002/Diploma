using TMPro;
using UnityEngine;

public class PlayerStatsInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmountMoney;
    [SerializeField] private TextMeshProUGUI textAmountPassedLevels;

    private void Start()
    {
        textAmountMoney.text = GameData.Data.AmountMoney.ToString();
        textAmountPassedLevels.text = GameData.Data.AmountPassedLevels.ToString();
    }
}
