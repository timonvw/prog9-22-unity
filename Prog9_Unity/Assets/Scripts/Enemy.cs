using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IScoreable, IReset
{
    [SerializeField] private int score;
    public int scorePoints { get; set; }
    private bool scored = false;
    private float mass;
    
    public void OnScore() {
        scored = true;
    }

    private void Start() {
        scorePoints = score;
        mass = GetComponent<Rigidbody>().mass;
    }

    private void Update() {
        if (!scored) return;
        transform.localScale -= new Vector3(mass, mass, mass) * Time.deltaTime;
        if (transform.localScale.x <= 0) {
            OnReset();
        }
    }
    
    public void OnReset() {
        GameMaster.Instance.SpawnNewEnemy();
        Destroy(gameObject);
    }
}
