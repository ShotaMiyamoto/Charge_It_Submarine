using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Player; //プレイヤー
    private int numOfGenerate = 0; //いくつマップを生成したか
    public GameObject[] coursePrefabs; //生成するコースプレファブ
    private float currentCourseEndPos = 0f; //最後に生成したコースプレファブの終点（次のコースをくっつけるZ座標）
    private int courseChangeCondition = 10;　//コースチェンジする条件
    [SerializeField] private GameObject bossArea; //コースチェンジする時に入れられるボスエリア
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //生成したコースの終点がプレイヤーのZ座標+3000ｍ以内になったら新しいマップ生成
        if(Player.transform.position.z + 3000f > currentCourseEndPos)
        {
            Debug.Log(currentCourseEndPos);

            if (numOfGenerate > courseChangeCondition) //生成数がコースチェンジ条件数以上であればボスエリア生成
            {
                Instantiate(bossArea, new Vector3(0, 0, currentCourseEndPos), bossArea.transform.rotation); //生成
                currentCourseEndPos += 1000f; //次の生成位置をプラス1000ｍする
                numOfGenerate++;　//生成したコース数をプラス
                courseChangeCondition += 10; //次のボスエリア位置を設定
            }
            else
            { 　//生成数がコースチェンジ条件数に満たなければ通常コース生成
                int GenCourseNum = Random.Range(0, coursePrefabs.Length); //生成するコースの番号を決定
                Instantiate(coursePrefabs[GenCourseNum], new Vector3(0, 0, currentCourseEndPos), coursePrefabs[GenCourseNum].transform.rotation); //生成
                currentCourseEndPos += 1000f; //次の生成位置をプラス1000ｍする
                numOfGenerate++; //生成したコース数をプラス
                Debug.Log(GenCourseNum + "番コース生成");
            }
        }
    }
}
