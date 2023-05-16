using TMPro;
using UnityEngine;

public class PlayerStatsInformation : Singleton<PlayerStatsInformation>
{
    [SerializeField] private TextMeshProUGUI textAmountMoney;
    [SerializeField] private TextMeshProUGUI textAmountPassedLevels;

    private void Start()
    {
        UpdateInformationText();
    }

    public void UpdateInformationText()
    {
        textAmountMoney.text = GameData.Data.AmountMoney.ToString();
        textAmountPassedLevels.text = GameData.Data.AmountPassedLevels.ToString();
    }
}
