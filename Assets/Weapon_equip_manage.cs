using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon_equip_manage : MonoBehaviour
{
    GameObject player1;
    GameObject playerEquipPoint1;

    GameObject player2;
    GameObject playerEquipPoint2;

    bool isPlayer1Enter;
    bool isPlayer2Enter;

    float timer1p = 0;
    float timer2p = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        player1 = GameObject.FindWithTag("Player1");
        playerEquipPoint1 = GameObject.FindWithTag("EquipPoint1"); 
        player2 = GameObject.FindWithTag("Player2");
        playerEquipPoint2 = GameObject.FindWithTag("EquipPoint2");
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent == null)  // 총이 돌아가는 시각적 효과
        {
            this.transform.Rotate(0, 0.1f, 0);            
        }
        timer = timer + Time.deltaTime;
        //player 1
        if (Input.GetButton("Equip1") && isPlayer1Enter && !player_move.is1pHandsOn)
        {
            //손에 든게 없을 때 총을 주움
            transform.SetParent(playerEquipPoint1.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = playerEquipPoint1.transform.rotation;
        }
        if (player_move.is1pHandsOn)
        {
            timer1p = timer1p + Time.deltaTime;
        }
        if (Input.GetButton("Equip1") && player_move.is1pHandsOn && timer1p>0.5f && this.transform.parent) //이미 손에 있을 때 총을 버림
        {
            Destroy(this.gameObject);
        }
        else if (!player_move.is1pHandsOn)
        {
            timer1p = 0;
        }

        //player 2
        if (Input.GetButton("Equip2") && isPlayer2Enter && !player_move.is2pHandsOn)
        {
            //손에 든게 없을 때 총을 주움
            transform.SetParent(playerEquipPoint2.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = playerEquipPoint2.transform.rotation;
        }
        if (player_move.is2pHandsOn)
        {
            timer2p = timer2p + Time.deltaTime;
        }
        else if (!player_move.is2pHandsOn)
        {
            timer2p = 0;
        }
        if (Input.GetButton("Equip2") && player_move.is2pHandsOn && timer2p > 0.5f && this.transform.parent) //이미 손에 있을 때 총을 버림
        {
            Destroy(this.gameObject);
        }
        

        if (timer > 10)
        {
            if (!this.transform.parent)       //소유하는 플레이어가 없으면 생성 후 10초 뒤 삭제
            {
                Destroy(this.gameObject);
            }
            else
            {
                if (timer1p > 10.0f || timer2p > 10.0f) { // 플레이어가 소유한 시점부터 10초 후 삭제
                    Destroy(this.gameObject);
                }
            }
        }

        if (this.transform.parent != null)         
        {
            var pm = this.transform.root.GetComponent<player_move>();
            if (!pm.is_life)         // DIE
            {
                Destroy(this.gameObject);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            isPlayer1Enter = true;
        }
        if (other.gameObject.tag == "Player2")
        {
            isPlayer2Enter = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            isPlayer1Enter = false; 
        }
        if (other.gameObject.tag == "Player2")
        {
            isPlayer2Enter = false;
        }
    }

}
