using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    #region Singleton
    private static GameMaster instance;
    public static GameMaster Instance {
        get {
            if (instance == null) {
                GameObject obj = new GameObject("GameMaster");
                obj.AddComponent<GameMaster>();
            }
            return instance;
        }
    }
    #endregion

    public int score;
    public float timeLeft;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<Transform> spawnPoints;

    private void Awake() {
        instance = this;
        score = 0;
        timeLeft = 120f;
    }
    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < spawnPoints.Count; i++) {
            int randomEnemyIndex = Random.Range(0, enemies.Count);
            Instantiate(enemies[randomEnemyIndex], spawnPoints[i].position, Quaternion.identity);
        }
        StartCoroutine(GameCountDown());
    }

    public void SpawnNewEnemy() {
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        int randomEnemyIndex = Random.Range(0, enemies.Count);
        Instantiate(enemies[randomEnemyIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
    }

    private IEnumerator GameCountDown() {
        yield return new WaitForSeconds(timeLeft);
        InputManager.Instance.EnableGameplay(false);
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
