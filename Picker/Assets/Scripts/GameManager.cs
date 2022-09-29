using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class BallArea
{
    public Animator BallArea_Elevator;
    public TextMeshProUGUI NumberText;
    public int CollectBalls;
    public GameObject[] Balls;
    public GameObject[] Cubes;
    public GameObject[] Capsules;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObject;
    [SerializeField] private GameObject BallControl;
    public bool StatusPicker;

    public float limit;

    public Slider slider;
    public GameObject interaction;
    bool isFnish = false;

    int CollectBalls_Number;
    int AllCheckPointCount;
    int CurrentCheckPointIndex;
    float FingerPosX;

    public GameObject[] Panels;  

    [SerializeField] private List<BallArea> _BallArea = new List<BallArea>();

    void Start()
    {
        StatusPicker = true;
        for (int i = 0; i < _BallArea.Count; i++)
        {
            _BallArea[i].NumberText.text = CollectBalls_Number + "/" + _BallArea[i].CollectBalls;
        }

        AllCheckPointCount = _BallArea.Count - 1;
        Time.timeScale = 0;

        float difference = Vector3.Distance(PickerObject.transform.position, interaction.transform.position);
        slider.maxValue = difference;
    }
    void Update()
    {
        float difference = Vector3.Distance(PickerObject.transform.position, interaction.transform.position);
        slider.value = difference;
        if (difference<=0.2f)
        {
            isFnish = true;
        }
        float xPos = Mathf.Clamp(PickerObject.transform.position.x, -limit, limit);
        //  PickerObject.transform.position = new Vector3(xPos, transform.position.y, );
        if (StatusPicker)
        {
            PickerObject.transform.position += 5f * Time.deltaTime * PickerObject.transform.forward;

            if (Time.timeScale != 0)
            {

                /* if (Input.touchCount > 0)
                 {
                     Touch touch = Input.GetTouch(0);

                     Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10f));
                     switch (touch.phase)
                     {
                         case TouchPhase.Began:
                             FingerPosX = TouchPosition.x - PickerObject.transform.position.x;

                             break;

                         case TouchPhase.Moved:
                             if(TouchPosition.x- FingerPosX> -1.15 && TouchPosition.x- FingerPosX< 1.15)
                             {
                                 PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position,
                                     new Vector3(TouchPosition.x - FingerPosX, PickerObject.transform.position.y,
                                     PickerObject.transform.position.z), 3f);
                             }

                             break; 
                     }
                 } */

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3
                      (xPos - 0.1f, PickerObject.transform.position.y,
                      PickerObject.transform.position.z), 0.2f);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3
                      (xPos + 0.1f, PickerObject.transform.position.y,
                      PickerObject.transform.position.z), 0.2f);
                }
            }
            else 
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Panels[2].SetActive(false);
                    Time.timeScale = 1;
                }         
            }
        }
    }
    public void Win()
    {
        Panels[0].SetActive(true);
    }
    public void Lose()
    {
        Panels[1].SetActive(true);       
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnBorder()
    {
        StatusPicker = false;     
        Invoke("StageControl", 2f);
        Collider[] HitColl = Physics.OverlapBox(BallControl.transform.position, BallControl.transform.localScale / 2,
            Quaternion.identity);
        int i = 0;
        while (i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, .6f), ForceMode.Impulse);
            i++;
        }
    }
    public void BallCounter()
    {
        CollectBalls_Number++;
        _BallArea[CurrentCheckPointIndex].NumberText.text = CollectBalls_Number + "/" 
            + _BallArea[CurrentCheckPointIndex].CollectBalls;
    }
    void StageControl()
    {
        if (CollectBalls_Number >= _BallArea[CurrentCheckPointIndex].CollectBalls)
        {
            _BallArea[CurrentCheckPointIndex].BallArea_Elevator.Play("Elevator");

            if (_BallArea[2]== _BallArea[CurrentCheckPointIndex])
            {
                Win();
            }
            foreach (var item in _BallArea[CurrentCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }
            foreach (var item in _BallArea[CurrentCheckPointIndex].Cubes)
            {
                item.SetActive(false);
            }
            foreach (var item in _BallArea[CurrentCheckPointIndex].Capsules)
            {
                item.SetActive(false);
            }
            if (CurrentCheckPointIndex == AllCheckPointCount)
            {
               // Debug.Log("Game Over");
               // Lose();
                Time.timeScale = 0;
            }
            else
            {           
                CurrentCheckPointIndex++;
                CollectBalls_Number = 0;           
            }
        }
        else
        {
            Debug.Log("Kayb");
            Lose();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(BallControl.transform.position, BallControl.transform.localScale);
    }
}
