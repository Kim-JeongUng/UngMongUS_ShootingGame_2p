using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_scene : MonoBehaviour
{
    public static int WinCnt1p = 0; //승리 횟수 카운트 (게임 씬 불러오기시 초기화 방지)
    public static int WinCnt2p = 0;

    public void ChangeStartScene()
    {
        SceneManager.LoadScene("StartScene");

    }
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
