using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour {
    public Text score;
    public Text time;
    public GameObject finalScoreObj;
    public Text finalScore;

    // Update is called once per frame
    void Update() {
        if (GameMaster.Instance.timeLeft >= 0) {
            GameMaster.Instance.timeLeft -= Time.deltaTime;
        } else {
            GameMaster.Instance.timeLeft = 0;
            finalScore.text = "Your score: " + GameMaster.Instance.score;
            finalScoreObj.SetActive(true);
        }
        score.text = "Score: " + GameMaster.Instance.score;
        time.text = "Time: " + GameMaster.Instance.timeLeft.ToString("0.0");
    }
}
