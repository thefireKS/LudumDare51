using System.Collections;
using TMPro;
using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField] private float speed, lifetime;
    [SerializeField] private TextMeshProUGUI scorePopUp_TMP;

    private CanvasGroup _canvasGroup;

    [Header("Transparency")] 
    [SerializeField] private float startTransparency;
    [SerializeField] private float endTransparency; 
    [SerializeField] private float transparencyTime;
    
    private void Awake()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        
        Destroy(gameObject, lifetime);
        StartCoroutine(TransparencyOverTime(startTransparency, endTransparency, transparencyTime));
    }

    private void Update()
    {
        transform.position += transform.up * (speed * Time.fixedDeltaTime);
    }
    private IEnumerator TransparencyOverTime(float startTransValue, float endTransValue, float transTimeValue)
    {
        float elapsedTime = 0f;
        
        // Пока прошло меньше времени, чем заданная продолжительность
        while (elapsedTime < transTimeValue)
        {
            // Интерполяция между startScaleValue и endScaleValue
            _canvasGroup.alpha = Mathf.Lerp(startTransValue, endTransValue, elapsedTime / transTimeValue);

            // Увеличиваем прошедшее время
            elapsedTime += Time.deltaTime;

            // Ждем до следующего кадра
            yield return null;
        }

        // Обеспечение точной установки финального значения масштаба
        _canvasGroup.alpha = endTransValue; 
    }

    public void SetText(int score)
    {
        scorePopUp_TMP.text = $"+{score}";
    }
}