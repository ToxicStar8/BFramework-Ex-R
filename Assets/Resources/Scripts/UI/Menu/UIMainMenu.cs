/*********************************************
 * 
 * 脚本名：UIMainMenu.cs
 * 创建时间：2023/12/20 16:49:39
 *********************************************/
using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public partial class UIMainMenu : GameUIBase
    {
        public override void OnInit()
        {
            Btn_CreateGame.AddListener(OnClick_Btn_CreateGame);
            Btn_JoinGame.AddListener(OnClick_Btn_JoinGame);
            Btn_Setting.AddListener(OnClick_Btn_Setting);
            Btn_Achievement.AddListener(OnClick_Btn_Achievement);
            Btn_Exit.AddListener(OnClick_Btn_Exit);

            RegisterUpdate(Update);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

            }

            if (Input.GetKeyDown(KeyCode.G))
            {

            }

            if (Input.GetKeyDown(KeyCode.H))
            {

            }
        }

        public override void OnShow(params object[] args)
        {

        }

        private void OnClick_Btn_CreateGame()
        {
            Log("点击了Btn_CreateGame");
        }

        private void OnClick_Btn_JoinGame()
        {
            Log("点击了Btn_JoinGame");

        }

        private void OnClick_Btn_Setting()
        {
            Log("点击了Btn_Setting");
        }

        private void OnClick_Btn_Achievement()
        {
            Log("点击了Btn_Achievement");
        }

        private void OnClick_Btn_Exit()
        {
            Application.Quit();
        }

        public override void OnBeforeDestroy() { }
    }
}
