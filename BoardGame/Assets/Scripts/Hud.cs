using UnityEngine;
using UnityEngine.UI;

// 情報表示用の UI を制御するコンポーネント
public class Hud : MonoBehaviour
{
    public Image m_p_hpGauge; // HP ゲージ
    public Image m_e_hpGauge; // HP ゲージ
    public GameObject m_gameOverText; // ゲームオーバーのテキスト
    public GameObject m_gameClearText; // ゲームクリアのテキスト
    public GameObject m_special; // ゲームクリアのテキスト
    public GameObject m_fever; // ゲームクリアのテキスト
    public Player player;
    public Enemy enemy;
    public Text sp;
    public Text com;
    public Text score;
    public GameObject im;
    public int rate1, rate2;

    // 毎フレーム呼び出される関数
    private void Update()
    {
        sp.text = player.GetSp().ToString();
        com.text = player.com;
        // HP のゲージの表示を更新する
        var php = player.GetHp();
        var phpMax = player.maxHp;
        m_p_hpGauge.fillAmount = (float)php / phpMax;

        // HP のゲージの表示を更新する
        var ehp = enemy.GetHp();
        var ehpMax = enemy.maxHp;
        m_e_hpGauge.fillAmount = (float)ehp / ehpMax;

        m_special.SetActive(player.IsSp());
        m_gameOverText.SetActive(!player.gameObject.activeSelf);
        m_gameClearText.SetActive(!enemy.gameObject.activeSelf);
        m_fever.SetActive(player.count > 0);
        im.SetActive(player.IsSp());
    }

    public void GameOver()
    {
        var point = (int) (player.gameTimer / rate1) + player.GetHp() * rate2;
        score.text += point.ToString();
        score.gameObject.SetActive(true);
    }
}