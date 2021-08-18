using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    public int scorepoint = 10;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject gm = GameObject.Find("GameManager");
            gm.GetComponent<GameManager>().AddScore(scorepoint);
        }
    }
}
