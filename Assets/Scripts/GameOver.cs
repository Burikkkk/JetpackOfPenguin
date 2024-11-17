using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private GameProgress gameProgress;

    void Start()
    {
        gameProgress = FindObjectOfType<GameProgress>();
        if (gameProgress != null)
        {
            gameProgress.ShowProgressOnGameOver();
        }
    }
}
