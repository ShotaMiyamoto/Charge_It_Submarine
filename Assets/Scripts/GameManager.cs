using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; //Action用
using Cinemachine;

public enum GameState
{
    Title,
    Playing,
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState currentGameState; //現在のステート
    public Text gameStateText; //ゲームステートテキスト

    //＝＝＝＝＝＝＝＝＝＝＝＝＝CourseGenerator関係＝＝＝＝＝＝＝＝＝＝＝＝＝
    public CourseGenerator courseGenerator; 

    //＝＝＝＝＝＝＝＝＝＝＝＝＝タイトルステート関係＝＝＝＝＝＝＝＝＝＝＝＝＝
    public GameObject titleText; 
    public GameObject UIObject;
    public CinemachineVirtualCamera frontCamera;
    private bool canTapToStart = false;
    
    //起動時にタイトルステートにする
    private void Awake()
    {
        Instance = this;
        SetCurrentState(GameState.Title);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentGameState == GameState.Title && canTapToStart)
            {
                SetCurrentState(GameState.Playing);
            }
        }
    }

    //外からこのメソッドを使って状態を変更
    public void SetCurrentState(GameState state)
    {
        currentGameState = state;
        OnGameStateChanged(currentGameState);
    }

    //状態が変わったら何をするか
    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Title: //タイトル
                TitleAction();
                break;

            case GameState.Playing: //プレイ中
                PlayingAction();
                break;

            case GameState.End: //エンディング
                EndAction();
                break;

            default:
                break;
        }
    }

    //Titleになったときの処理
    void TitleAction()
    {
        gameStateText.text = "タイトル";
        canTapToStart = false;
        courseGenerator.OnGenerateStateChanged(CourseGenerator.GenerateState.Title); //コース生成ステートを設定
        titleText.SetActive(true); //タイトルを有効化
        UIObject.SetActive(false); //UIオブジェクト無効化
        frontCamera.enabled = true; //フロントカメラを有効化

        StartCoroutine(DelayMethod(1.0f, () => //1秒後にタップ可能にする
         {
             canTapToStart = true;
         }));
    }

    //Playingになったときの処理
    void PlayingAction()
    {
        gameStateText.text = "プレイ中";
        courseGenerator.OnGenerateStateChanged(CourseGenerator.GenerateState.Random); //コース生成ステートを設定
        frontCamera.enabled = false; //フロントカメラを無効化
        titleText.SetActive(false); //タイトルを無効化

        StartCoroutine(DelayMethod(2.0f, () => //2秒後にUIオブジェクトを有効化
        {
            UIObject.SetActive(true); //UIオブジェクト有効化
        }));
    }

    //Endになったときの処理
    void EndAction()
    {
        
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
