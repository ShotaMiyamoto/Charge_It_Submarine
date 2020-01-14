using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseGenerator : MonoBehaviour
{
    public GameObject Player; //プレイヤー
    private int numOfGenerate = 0; //いくつマップを生成したか
    public GameObject[] coursePrefabs; //生成するコースプレファブ
    private float currentCourseEndPos = 0f; //最後に生成したコースプレファブの終点（次のコースをくっつけるZ座標）
    public GameObject bossArea; //コースチェンジする時に入れられるボスエリア

    public enum GenerateState
    {
        Title,
        Random,
        Boss
    }

    public GenerateState currentGenerateState;

    // Start is called before the first frame update
    void Start()
    {
        currentGenerateState = GenerateState.Title;
    }

    // Update is called once per frame
    void Update()
    {
        //10の倍数の時にボスエリアのステートにする
        if (numOfGenerate != 0 && numOfGenerate % 10 == 0)
        {
           currentGenerateState = GenerateState.Boss;
        }

        //生成したコースの終点がプレイヤーのZ座標+3000ｍ以内になったら新しいマップ生成
        if(Player.transform.position.z + 3000f > currentCourseEndPos)
        {
            //Debug.Log(currentCourseEndPos);
            
            switch (currentGenerateState) //ステートによって切り替える
            {
                case GenerateState.Title:

                    Instantiate(coursePrefabs[0], new Vector3(0, 0, currentCourseEndPos), coursePrefabs[0].transform.rotation); //生成
                    currentCourseEndPos += 1000f; //次の生成位置をプラス1000ｍする
                    //ここではコース生成数はプラスしない。プレイステートになってから数え始める。

                    break;

                case GenerateState.Random:

                    //生成数がコースチェンジ条件数に満たなければ通常コース生成
                    if (numOfGenerate == 0) //最初の生成は直線コース
                    {
                        Instantiate(coursePrefabs[0], new Vector3(0, 0, currentCourseEndPos), coursePrefabs[0].transform.rotation); //生成
                        currentCourseEndPos += 1000f; //次の生成位置をプラス1000ｍする
                        numOfGenerate++; //生成したコース数をプラス
                        //Debug.Log("直線コース生成");
                    }
                    else
                    {
                        int GenCourseNum = Random.Range(0, coursePrefabs.Length); //生成するコースの番号を決定
                        Instantiate(coursePrefabs[GenCourseNum], new Vector3(0, 0, currentCourseEndPos), coursePrefabs[GenCourseNum].transform.rotation); //生成
                        currentCourseEndPos += 1000f; //次の生成位置をプラス1000ｍする
                        numOfGenerate++; //生成したコース数をプラス
                        //Debug.Log(GenCourseNum + "番コース生成");
                    }

                    break;

                case GenerateState.Boss:

                    Instantiate(bossArea, new Vector3(0, 0, currentCourseEndPos), bossArea.transform.rotation); //生成
                    currentCourseEndPos += 1000f; //次の生成位置をプラス1000ｍする
                    numOfGenerate++; //生成したコース数をプラス
                    currentGenerateState = GenerateState.Random;
                    //Debug.Log("ボスコース生成");

                    break;
            }
        }
    }

    public void OnGenerateStateChanged(GenerateState state)
    {
        currentGenerateState = state;
    }
}
