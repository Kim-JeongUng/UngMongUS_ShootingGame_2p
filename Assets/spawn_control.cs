using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawn_control : MonoBehaviour
{
    public Transform[] spawn_point;
    public GameObject[] ITEM_NAME;
    public GameObject[] SPECIAL_ITEM_NAME;
    public GameObject prefab_player1;
    public GameObject prefab_player2;


    void Start()
    {
        GameObject player1 = Instantiate(prefab_player1)as GameObject;
        GameObject player2 = Instantiate(prefab_player2) as GameObject;

        InvokeRepeating("Spawnitem", 1, 5); //5초마다 생성
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //R 키를 누르면 새로시작
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void Spawnitem()
    {
        int random_weapons = Random.Range(0,5);   //m4, shotgun, pistoll, hppotion, speedup
        int random_spawn = Random.Range(0, 20);
        int special_weapons = Random.Range(0, 10); // 0 : bomb launcher 1 : rail gun  2: beam gun   else : none

        if (true)
        {
            if (random_weapons == 5) //special weapon
            {
                random_spawn = 20; // special_spawn
            }
            if (random_weapons == 3 || random_weapons == 4)
            {   //포션
                GameObject item = (GameObject)Instantiate(ITEM_NAME[random_weapons], spawn_point[random_spawn].transform.position - Vector3.up, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                Destroy(item, 10.0f); //생성 10초 뒤 아이템 소멸
            } //무기 destroy : weapon_equip_manage_script :: 80 line
            else
            {
                GameObject item = (GameObject)Instantiate(ITEM_NAME[random_weapons], spawn_point[random_spawn].transform.position, this.transform.rotation) as GameObject;
            }

            if(special_weapons <=2) //스페셜 무기는 지정된 장소(우측 무기고)에서만 출현
            {
                GameObject spitem = (GameObject)Instantiate(SPECIAL_ITEM_NAME[special_weapons], spawn_point[20].transform.position, this.transform.rotation) as GameObject;
            }
        }
        
    }
    void OnGUI() //승리 횟수 카운트 GUI
    {
        GUI.Label(new Rect(Screen.width / 2 - 270, 0, 100, 50), "<size=30>2p : " + start_scene.WinCnt2p.ToString() + "</size>");
        GUI.Label(new Rect(Screen.width / 2 + 80, 0, 100, 50), "<size=30>1p : " + start_scene.WinCnt1p.ToString() + "</size>");
    }
}