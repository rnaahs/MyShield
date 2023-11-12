using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Square;
    public GameObject EndPanel;
    public Text ThisScoreText;
    public Text MaxScoreText;
    public Text TimeTxt;
    public Animator anim;
    float Alive = 0f;
    bool IsRunning = true;
    public static GameManager I;
    

    private void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("makeSquare", 0, 0.5f);
    }

    void makeSquare()
    {
        Instantiate(Square);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRunning)
        {
            Alive += Time.deltaTime;
            TimeTxt.text = Alive.ToString("N2");
        }
    }

    public void GameOver()
    {
        IsRunning = false;
        anim.SetBool("IsDie", true); //애니메이터 파라미터 IsDie 에 True 할당
        Invoke("TimeStop", 0.5f);
        EndPanel.SetActive(true); //Endpanel 활성화
        ThisScoreText.text = Alive.ToString("N2");
        if(!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetFloat("BestScore", Alive);
        }
        else
        {
            if(Alive > PlayerPrefs.GetFloat("BestScore"))
            {
                PlayerPrefs.SetFloat("BestScore", Alive);
            }
        }
        float maxScore = PlayerPrefs.GetFloat("BestScore");
        MaxScoreText.text = maxScore.ToString("N2");
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
}
