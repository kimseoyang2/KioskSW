using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    Coroutine InputChecker;
    public bool isUserInput = false;
    public bool isTimeout = false;
    public bool isClicked = false;

    public float setTime = 10;

    public int state;

    Coroutine runningCoroutine = null;
    IEnumerator startCoroutine;

    // Start is called before the first frame update
    void Start()
    {





        startCoroutine = ForStart();
        StartCoroutine(startCoroutine);







    }

    // Update is called once per frame
    void Update()
    {
        www_Req www_req = GameObject.Find("WW").GetComponent<www_Req>();


        // appState 값이 다를때만 값 오버로드 
        if (state != www_req.appState)
        {
            state = www_req.appState;
        }





        // appState가 0인데 인간 움직임이 있다 ? ====> 1로 전환
        //if (www_req.appState == 0 && isUserInput)
        //{
        // www_req.appState = 1;
        //}

        // appState가 1이고 인간 움직임이 없다  ? ======> 0으로 전환
        // else if(www_req.appState == 1 && isUserInput==false)
        //{
        //www_req.appState = 0;
        // }



        // 터치스크린에서 되는지 확인할 것 
        if (Input.GetMouseButtonDown(0))

        {
            StopCoroutine(startCoroutine);

            isClicked = true;
            //사용자의 인풋값이 들어오면 시작하면서 발생하는 코루틴은 끈다.
            if (isClicked)
            {
                StopCoroutine(ForStart());
            }

            www_req.appState = 1;


            isUserInput = true;
            isTimeout = false;


            print("타이머재시작");

            // 진행중인 코루틴이 있다면 멈춘다.
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
            }

            //코루틴을 시작하며, 동시에 저장한다.    
            runningCoroutine = StartCoroutine(co_Timer());

        }



        if (isTimeout == true)
        {
            StartCoroutine(ToIndex());

        }




    }


    //자바스크립트의 Decide를 가지고 오는 함수
    public void GetDecide()
    {
        www_Req www_req = GameObject.Find("WW").GetComponent<www_Req>();


        //state=2로 변경하고
        www_req.appState = 2;
        www_req.POST();

        // JavaScript의 값을 가지고 온다.
        www_req.GET();



    }


    //리셋 버튼용 함수
    public void Force_Quit()
        {
        SceneManager.LoadScene(0);
        }


    //실행 내내 계속 돌아가는 코루틴 함수, 움직임이 있을때마다 재 로드된다. 
    IEnumerator co_Timer()
    {


        yield return new WaitForSeconds(setTime);

        isTimeout = true;
        isUserInput = false;




       
     







    }

    // 값을 제대로 할당하고 가기 위해 넣는 안심용 코루ㅌ
    IEnumerator ToIndex()
    {
        www_Req www_req = GameObject.Find("WW").GetComponent<www_Req>();

        // www_req.POST();
        // www_req.GET();
        // 위 논리식에 해당 안되므로 강제 부여
        

        if (state != 0)
        {
            www_req.appState = 0;
            www_req.POST();
            www_req.GET();

            //isUserInput = true;
        }

        //isUserInput = false;
        print("잠시 후 다음 씬으로 이동합니다.");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }



    IEnumerator ForStart()
    {


        yield return new WaitForSeconds(10);
        Force_Quit();






    }

}








    




    