/*********************************************
 * 自动生成代码，禁止手动修改文件
 * 脚本名：UIMainMenu.Design.cs
 * 修改时间：2024/01/08 15:54:02
 *********************************************/

using Framework;
using UnityEngine;
using UnityEngine.UI;

namespace GameData
{
    public partial class UIMainMenu
    {
        /// <summary>
        /// 
        /// </summary>
        public UnityEngine.UI.Button Btn_CreateGame;

        /// <summary>
        /// 
        /// </summary>
        public UnityEngine.UI.Button Btn_JoinGame;

        /// <summary>
        /// 
        /// </summary>
        public UnityEngine.UI.Button Btn_Setting;

        /// <summary>
        /// 
        /// </summary>
        public UnityEngine.UI.Button Btn_Achievement;

        /// <summary>
        /// 
        /// </summary>
        public UnityEngine.UI.Button Btn_Exit;

        public override void OnCreate()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            Btn_CreateGame = rectTransform.Find("Btns/Btn_CreateGame").GetComponent<UnityEngine.UI.Button>();
			Btn_JoinGame = rectTransform.Find("Btns/Btn_JoinGame").GetComponent<UnityEngine.UI.Button>();
			Btn_Setting = rectTransform.Find("Btns/Btn_Setting").GetComponent<UnityEngine.UI.Button>();
			Btn_Achievement = rectTransform.Find("Btns/Btn_Achievement").GetComponent<UnityEngine.UI.Button>();
			Btn_Exit = rectTransform.Find("Btns/Btn_Exit").GetComponent<UnityEngine.UI.Button>();
			
        }
    }
}
