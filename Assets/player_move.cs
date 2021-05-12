using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_move : MonoBehaviour //캐릭터의 움직임과 HP관리
{
    public KeyCode left;         //key
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    public int maxHp;           
    public int nowHp;
    public int speed;

    public GameObject fp_hp_bar;         //체력바
    public Image nowHpBar;
    RectTransform hpBar;

    GameObject playerEquipPoint;         //총 장착 위치

    private Animator anim;

    public static bool is1pHandsOn;
    public static bool is2pHandsOn;

    public bool is_life; // hp>0 일때 true

    bool healtrue;          //힐링존에서의 체력회복
    float healtimer = 0;

    bool play_game; // 죽은 후 한번만 승리 횟수를 카운트 하기 위함

    public AudioClip DeadSound;							

    void Start()
    {
        speed = 10;
        maxHp = 100;
        nowHp = 100;
        anim = GetComponent<Animator>();
        hpBar = fp_hp_bar.GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();

        is_life = true;
        is1pHandsOn = false;
        is2pHandsOn = false;

        play_game = true;

        healtrue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowHp > 0)
        {
            Move();
            if (healtrue) //힐링존 초당 2씩 체력 회복
            {
                healtimer = healtimer + Time.deltaTime;
                if (healtimer > 1)
                {
                    nowHp = nowHp + 2;
                    healtimer = 0;
                }
            }
            is_life = true;
        }
        else if(nowHp <= 0)
        {
            DIE();
        }
        HandsOnCheck();
        if (nowHp > maxHp)
            nowHp = maxHp;
        nowHpBar.fillAmount = (float)nowHp / (float)maxHp;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.transform.tag)
        {
            case "Potion":  //Hp 포션 체력 20 향상
                nowHp = nowHp + 20;
                Destroy(other.gameObject);
                break;
            case "SpeedPotion": //영구적 속도 2 향상
                speed = speed + 2;
                Destroy(other.gameObject);
                break;
            case "healing":
                Debug.Log("HealingOn");
                healtimer = 0;
                healtrue = true;
                break;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "healing")
        {
            Debug.Log("HealingOff");
            healtimer = 0;
            healtrue = false;
        }
    }
    void Move()
    {

        if (Input.GetKey(left) || (Input.GetKey(right)) || (Input.GetKey(up)) || (Input.GetKey(down)))
        {
            if (Input.GetKey(left) == true)
            {
                this.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                transform.localScale = new Vector3(-2, 2, 2);
            }
            if (Input.GetKey(right) == true)
            {
                this.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
                transform.localScale = new Vector3(2, 2, 2);
            }
            if (Input.GetKey(up) == true)
            {
                this.transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
            }
            if (Input.GetKey(down) == true)
            {
                this.transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
            }
                anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
    }
    void DIE()
    {
        anim.SetBool("die", true);
        is_life = false;

        if (this.transform.tag == "Player1" && play_game)
        {
            GetComponent<AudioSource>().PlayOneShot(DeadSound);
            start_scene.WinCnt2p = start_scene.WinCnt2p + 1;
            play_game = false;
        }
        if (this.transform.tag == "Player2" && play_game)
        {
            GetComponent<AudioSource>().PlayOneShot(DeadSound);
            start_scene.WinCnt1p = start_scene.WinCnt1p + 1;
            play_game = false;
        }
    }
    void HandsOnCheck()
    {
        if (this.transform.tag == "Player1")
        {
            if (transform.GetChild(0).childCount == 1)
            {
                is1pHandsOn = true;
            }
            else if ((transform.GetChild(0).childCount == 0))
            {
                is1pHandsOn = false;
            }
        }
        if (this.transform.tag == "Player2")
        {
            if (transform.GetChild(0).childCount == 1)
            {
                is2pHandsOn = true;
            }
            else if ((transform.GetChild(0).childCount == 0))
            {
                is2pHandsOn = false;
            }
        }
    }
    void OnGUI()
    {
        if (nowHp <= 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 90, 0, 100, 20), "<size=15>Reset : R</size>");
        }
    }


}