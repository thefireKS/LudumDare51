using UnityEngine;

public class ScorePopUpSpawner : MonoBehaviour
{
    [SerializeField] private ScorePopUp prefab;
    [SerializeField] private RectTransform spawnPosition;

    private int lastScore;

    private void OnEnable()
    {
        OxygenAndPointsManager.ScoreChanged += SpawnScorePopUp;
    }

    private void OnDisable()
    {
        OxygenAndPointsManager.ScoreChanged -= SpawnScorePopUp;
    }

    private void SpawnScorePopUp(int score)
    {
        ScorePopUp scorePopUp =
            Instantiate(prefab, spawnPosition.transform.position, spawnPosition.rotation, spawnPosition);
        scorePopUp.SetText(Mathf.Abs(lastScore - score));
        lastScore = score;
    }
}