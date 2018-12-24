using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    InputField inputField;
    public Player player;
 
    //InputFieldコンポーネントの取得および初期化メソッドの実行
    void Start()
    {
        inputField = GetComponent<InputField>();

        InitInputField();
    }

    //入力文字列を処理
    public void InputLogger()
    {
        string inputValue = inputField.text;
        if (inputValue == player.com) player.SpComplete();
        InitInputField();
    }

    // InputFieldの初期化用メソッド
    void InitInputField()
    {
        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }
}