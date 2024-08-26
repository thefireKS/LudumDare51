using UnityEngine;
using UnityEngine.UI;

public class FillUIToFillPlayer : MonoBehaviour
{
    [SerializeField] private Image uiOxygenBar;
    [SerializeField] private Image playerOxygenBar;

    private void Update()
    {
        playerOxygenBar.fillAmount = uiOxygenBar.fillAmount;
    }
}
