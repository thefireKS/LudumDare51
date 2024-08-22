using System.Collections;
using TMPro;
using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshProUGUI scorePopUp_TMP;

    [SerializeField] private float startScale, endScale, scaleTime;

    private void Awake()
    {
        StartCoroutine(ScaleOverTime(startScale, endScale, scaleTime));
    }

    private void Update()
    {
        transform.position += transform.up * (speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private IEnumerator ScaleOverTime(float startScaleValue, float endScaleValue, float scaleTimeValue)
    {
        float elapsedTime = 0f;

        Vector3 starScale = new Vector3(startScaleValue, startScaleValue, startScaleValue);
        Vector3 endScale = new Vector3(endScaleValue, endScaleValue, endScaleValue);
        // Пока прошло меньше времени, чем заданная продолжительность
        while (elapsedTime < scaleTimeValue)
        {
            // Интерполяция между startScaleValue и endScaleValue
            transform.localScale = Vector3.Lerp(starScale, endScale, elapsedTime / scaleTimeValue);

            // Увеличиваем прошедшее время
            elapsedTime += Time.deltaTime;

            // Ждем до следующего кадра
            yield return null;
        }

        // Обеспечение точной установки финального значения масштаба
        transform.localScale = endScale;
    }

    public void SetText(int score)
    {
        scorePopUp_TMP.text = $"+{score}";
    }
}