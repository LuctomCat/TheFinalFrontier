using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("TextMeshPro UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI barricadeHealthText;

    public void SetTimer(float time)
    {
        if (timerText != null)
        {
            timerText.text = time.ToString("F2");
        }
    }

    public void SetBarricadeHealth(int current, int max)
    {
        if (barricadeHealthText != null)
        {
            barricadeHealthText.text = "Health: " + current + " / " + max;
        }
    }
}
