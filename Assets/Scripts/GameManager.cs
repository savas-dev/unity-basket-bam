using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    [SerializeField] private Image[] targetImages;
    [SerializeField] private Sprite missionCompleteSprite;
    [SerializeField] private int targetBallCount;
    int basketCount;
    

    public GameObject platform;
    public int bigValue = 5;
    [SerializeField] private GameObject basket;


    public TextMeshProUGUI timeText;


    [SerializeField] private GameObject[] panels;
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private AudioSource[] sounds;
    public bool isPause;
    public bool isWin;

    float fingerPosX;

    public GameObject[] backgroundList;

    private void Awake()
    {
        
    }

    private void Start(){

        int randBack = Random.Range(0, 5);

        if(randBack == 0)
        {
            backgroundList[0].SetActive(true);
        }

        if (randBack == 1)
        {
            backgroundList[1].SetActive(true);
        }

        if (randBack == 2)
        {
            backgroundList[2].SetActive(true);
        }

        if (randBack == 3)
        {
            backgroundList[3].SetActive(true);
        }

        if (randBack == 4)
        {
            backgroundList[4].SetActive(true);
        }


        isPause = false;

        if (PlayerPrefs.HasKey("Level"))
        {
            levelName.text = "Level " + PlayerPrefs.GetInt("Level");
        }
        else
        {
            levelName.text = "Level 1";
        }

        basket = GameObject.FindGameObjectWithTag("BasketObject");


        for (int i = 0; i < targetBallCount; i++){
            targetImages[i].gameObject.SetActive(true);
        }
    }

    private void Update(){

        if (!isPause)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                        touch.position.x, touch.position.y, 10
                    ));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerPosX = touchPosition.x - platform.transform.position.x;
                        break;

                    case TouchPhase.Moved:
                        if(touchPosition.x - fingerPosX > -0.929f && touchPosition.x - fingerPosX < -0.929f)
                        {
                            platform.transform.position = Vector3.Lerp(
                                platform.transform.position, new Vector3(
                                touchPosition.x - fingerPosX,
                                platform.transform.position.y,
                                platform.transform.position.z
                    ),
                            5f
                );
                        }
                        break;
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (platform.transform.position.x > -0.929f)
                {
                    platform.transform.position = Vector3.Lerp(
                    platform.transform.position, new Vector3(
                        platform.transform.position.x - .3f,
                        platform.transform.position.y,
                        platform.transform.position.z
                    ),
                    .050f
                );
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (platform.transform.position.x < 0.929f)
                {
                    platform.transform.position = Vector3.Lerp(
                    platform.transform.position, new Vector3(
                        platform.transform.position.x + .3f,
                        platform.transform.position.y,
                        platform.transform.position.z
                    ),
                    .050f
                );
                }
            }
        }

    }

    public void Basket(){
        basketCount++;
        targetImages[basketCount - 1].sprite = missionCompleteSprite;

        if(basketCount == targetBallCount){
            Win();
        }
    }

    public void GameOver(){

        if (!isWin)
        {
            panels[3].SetActive(true);
        }
    }

    public void Win()
    {
        isWin = true;
        panels[2].SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Invoke("WinDelay", 1.5f);
        
    }

    public void WinDelay()
    {
        Time.timeScale = 0;
    }

    public void Buttons(string value)
    {
        switch (value)
        {
            case "Stop":
                Time.timeScale = 0;
                panels[1].SetActive(true);
                isPause = true;
                break;
            case "Resume":
                Time.timeScale = 1;
                isPause = false;
                panels[1].SetActive(false);
                break;
            case "Try":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Next":
                SceneManager.LoadScene(Random.Range(1, 10));
                Time.timeScale = 1;
                break;
        }
    }


}
