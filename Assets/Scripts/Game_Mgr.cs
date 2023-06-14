using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    UserPlay_Ing = 0,   //유저가 선택을 고민하는 상태
    Result_Ing = 1,   //결과 보여주기 상태
    GameEnd = 2    //게임 종료 상태
}

public enum GawiBawiBo
{
    Gawi = 1,
    Bawi = 2,
    Bo = 3,
    Count
}

public class Game_Mgr : MonoBehaviour
{
    public GameObject Game_Panel;

    public Button m_Gawi_Btn;
    public Button m_Bawi_Btn;
    public Button m_Bo_Btn;
    public Button m_RePlay_Btn;

    public Text m_UserInfo_Text;
    public Text m_Result_Text;

    int m_Money = 500;

    [Header("--- Gameble ---")]
    public Text m_Gameble_Text;
    public Slider m_Gameble_Slider;
    int m_Gameble = 50;

    [Header("--- Direction ---")]
    public Sprite[] m_IconImg;
    public Image m_UserGBB_Img;
    public Image m_ComGBB_Img;
    public Text m_ShowResultText;

    [Header("--- DamageText ---")]
    public Transform m_HUD_Canvas = null;
    public GameObject m_DamagePrefab = null;
    public Transform m_SpawnTxtPos = null;

    [Header("--- Auto Button ---")]
    public Toggle m_AutoToggle = null;
    public Button m_NextStartBtn = null;

    float m_WaitTimer = 0.0f;

    GameState m_GameState = GameState.UserPlay_Ing;

    void Start()
    {
        if (m_Gawi_Btn != null)
            m_Gawi_Btn.onClick.AddListener(() => {
                BtnClickMethod(GawiBawiBo.Gawi);
            });

        if (m_Bawi_Btn != null)
            m_Bawi_Btn.onClick.AddListener(() => {
                BtnClickMethod(GawiBawiBo.Bawi);
            });

        if (m_Bo_Btn != null)
            m_Bo_Btn.onClick.AddListener(() =>
            {
                BtnClickMethod(GawiBawiBo.Bo);
            });

        if (m_RePlay_Btn != null)
            m_RePlay_Btn.onClick.AddListener(ReplayBtnClick);

        if (m_AutoToggle != null)
            m_AutoToggle.onValueChanged.AddListener(AutoToggleCheck);

        if (m_NextStartBtn != null)
            m_NextStartBtn.onClick.AddListener(NextStartClick);

    }

    void Update()
    {
        if (1.0f <= m_Gameble_Slider.value || m_Money < 50)
            m_Gameble = m_Money;
        else
            m_Gameble = 50 + (int)(m_Gameble_Slider.value * (m_Money - 50));

        if (m_Gameble_Text != null)
            m_Gameble_Text.text = "보유 금액 : " + m_Gameble;

        if (m_GameState == GameState.UserPlay_Ing)
        {
            ComAnimUpdate();
        }
        else if (m_GameState == GameState.Result_Ing)
        {
            if (m_AutoToggle.isOn == true)
                if (0.0f < m_WaitTimer)
                {
                    m_WaitTimer -= Time.deltaTime;

                    if (m_WaitTimer <= 0.0f)
                    {
                        m_GameState = GameState.UserPlay_Ing;

                        m_UserGBB_Img.gameObject.SetActive(false);
                        m_ShowResultText.gameObject.SetActive(false);
                    }
                }
        }
    }

    void BtnClickMethod(GawiBawiBo a_UserSel)
    {
        if (m_GameState != GameState.UserPlay_Ing)
            return;

        GawiBawiBo a_ComSel = (GawiBawiBo)Random.Range(
                              (int)GawiBawiBo.Gawi, (int)GawiBawiBo.Count);

        string a_strUser = "가위";
        if (a_UserSel == GawiBawiBo.Bawi)
            a_strUser = "바위";
        else if (a_UserSel == GawiBawiBo.Bo)
            a_strUser = "보";

        string a_strCom = "가위";
        if (a_ComSel == GawiBawiBo.Bawi)
            a_strCom = "바위";
        else if (a_ComSel == GawiBawiBo.Bo)
            a_strCom = "보";

        m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")";

        if (a_UserSel == a_ComSel)
        {
            m_Result_Text.text += " 비겼습니다.";
        }
        else if ((a_UserSel == GawiBawiBo.Gawi && a_ComSel == GawiBawiBo.Bo) ||
                 (a_UserSel == GawiBawiBo.Bawi && a_ComSel == GawiBawiBo.Gawi) ||
                 (a_UserSel == GawiBawiBo.Bo && a_ComSel == GawiBawiBo.Bawi))
        {
            m_Result_Text.text += " 승리하셨습니다.";

            m_Money += (m_Gameble * 2);

            SpawnDmgText((m_Gameble * 2), m_SpawnTxtPos.position,
                                            new Color32(130, 130, 255, 255));
        }
        else
        {
            m_Result_Text.text += " 패배하셨습니다.";

            m_Money -= m_Gameble;

            SpawnDmgText(-m_Gameble, m_SpawnTxtPos.position,
                                            new Color32(255, 130, 130, 255));

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }

        if (m_UserInfo_Text != null)
            m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money;

        Refresh_UI(a_UserSel, a_ComSel);
    }

    void Refresh_UI(GawiBawiBo a_U_Sel, GawiBawiBo a_C_Sel)
    {
        int a_USel_Idx = (int)a_U_Sel - 1;
        int a_CSel_Idx = (int)a_C_Sel - 1;

        if (a_USel_Idx < 0 || m_IconImg.Length <= a_USel_Idx)
            return;

        if (a_CSel_Idx < 0 || m_IconImg.Length <= a_CSel_Idx)
            return;

        if (m_UserGBB_Img != null)
        {
            m_UserGBB_Img.sprite = m_IconImg[a_USel_Idx];
            m_UserGBB_Img.gameObject.SetActive(true);
        }

        if (m_ComGBB_Img != null)
            m_ComGBB_Img.sprite = m_IconImg[a_CSel_Idx];

        if (a_U_Sel == a_C_Sel)
        {
            m_ShowResultText.color = new Color32(90, 90, 90, 255);
            m_ShowResultText.text = "무승부";
        }
        else if ((a_U_Sel == GawiBawiBo.Gawi && a_C_Sel == GawiBawiBo.Bo) ||
                 (a_U_Sel == GawiBawiBo.Bawi && a_C_Sel == GawiBawiBo.Gawi) ||
                 (a_U_Sel == GawiBawiBo.Bo && a_C_Sel == GawiBawiBo.Bawi))
        {
            m_ShowResultText.color = new Color32(0, 0, 255, 255);
            m_ShowResultText.text = "승리!!";
        }
        else
        {
            m_ShowResultText.color = new Color32(255, 0, 0, 255);
            m_ShowResultText.text = "패배..";
        }

        m_ShowResultText.gameObject.SetActive(true);

        m_GameState = GameState.Result_Ing;
        m_WaitTimer = 3.0f;

        if (m_Money <= 0)
            m_GameState = GameState.GameEnd;

    }

    float m_CycleTime = 0.0f;
    int m_ComIconIdx = 0;

    void ComAnimUpdate()
    {
        m_CycleTime += Time.deltaTime; 
        if (0.11f <= m_CycleTime)
        {
            m_CycleTime = 0.0f;

            m_ComIconIdx++;
            if (3 <= m_ComIconIdx)
                m_ComIconIdx = 0;

            if (m_ComGBB_Img == null || m_IconImg == null)
                return;

            if (m_ComIconIdx < 0 || m_IconImg.Length <= m_ComIconIdx)
                return;

            m_ComGBB_Img.sprite = m_IconImg[m_ComIconIdx];

        }
    }

    public void ReplayBtnClick()
    {
        Game_Panel.gameObject.SetActive(false);
    }

    void SpawnDmgText(float a_Value, Vector3 a_TxtPos, Color a_Color)
    {
        if (m_DamagePrefab == null || m_HUD_Canvas == null)
            return;

        GameObject a_DmgClone = (GameObject)Instantiate(m_DamagePrefab);
        a_DmgClone.transform.SetParent(m_HUD_Canvas, false);
        a_DmgClone.transform.position = a_TxtPos;

        DamageTxt a_DamageTx = a_DmgClone.GetComponent<DamageTxt>();
        if (a_DamageTx != null)
            a_DamageTx.InitDmgText(a_Value, a_Color);
    }

    void AutoToggleCheck(bool value)
    {
        Text a_Label = m_AutoToggle.GetComponentInChildren<Text>();

        if (value == true)
        {
            if (a_Label != null)
                a_Label.text = "자동";

            if (m_NextStartBtn != null)
                m_NextStartBtn.gameObject.SetActive(false);

            if (m_GameState == GameState.GameEnd)
                return;

            m_WaitTimer = 0.0f;
            m_GameState = GameState.UserPlay_Ing;
            m_UserGBB_Img.gameObject.SetActive(false);
            m_ShowResultText.gameObject.SetActive(false);
        }
        else
        {
            if (a_Label != null)
                a_Label.text = "수동";

            if (m_NextStartBtn != null)
                m_NextStartBtn.gameObject.SetActive(true);
        }
    }   

    void NextStartClick()
    {
        if (m_GameState != GameState.Result_Ing)
            return;

        m_GameState = GameState.UserPlay_Ing;
        m_UserGBB_Img.gameObject.SetActive(false);
        m_ShowResultText.gameObject.SetActive(false);
    }
}
