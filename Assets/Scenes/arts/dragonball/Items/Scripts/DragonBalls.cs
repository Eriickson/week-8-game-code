using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragonBalls : MonoBehaviour
{
    public float balls;
    // public Text dragonBallCount;
    private bool isInmune;

    public static DragonBalls instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DragonBall(float ball)
    {
        balls += ball;
        if(balls > 10)
        {
            SceneManager.LoadScene(0);
        }
        // dragonBallCount.text = "x " + balls.ToString();
        StartCoroutine(Inmune());
    }

    // Start is called before the first frame update
    void Start()
    {
        // dragonBallCount.text = "x " + balls.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Inmune()
    {
        isInmune = true;
        yield return new WaitForSeconds(1);
        isInmune = false;
    }
}
