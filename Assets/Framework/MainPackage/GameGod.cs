/*********************************************
 * BFramework
 * 游戏入口
 * 创建时间：2023/06/16 16:54:23
 *********************************************/
using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class GameGod : MonoBehaviour
    {
        /// <summary>
        /// UI根节点
        /// </summary>
        [SerializeField]
        public GameObject UIRoot;
        [SerializeField]
        public RectTransform UIRootRect;

        /// <summary>
        /// UI根节点下的层级
        /// </summary>
        private static Dictionary<E_UILevel, RectTransform> _uiRootDic;

        [SerializeField]
        public Camera UICamera;

        [SerializeField]
        public Transform ObjPool;

        [SerializeField]
        public Transform GameStart;

        [Header("加载界面")]
        [SerializeField]
        public WinLoading WinLoading;

#if UNITY_EDITOR
        [Header("时间倍率")]
        [SerializeField]
        public float TimeScale;
#endif

        private void OnInit()
        {
            //初始化表格
            TableManager.Init(TableTypes.TableCtrlTypeArr);
            //初始化Module
            ModuleManager.Init(ModuleTypes.ModuleTypeArr);
            //背景音乐
            AudioManager.PlayBackground("RetroComedy.ogg");
            //红点树启动
            var rootKey = RedPointManager.RootNode.Key;
            RedPointManager.AddOrGetNodeByParentKey<UIMainMenu>(rootKey);
            //正式启动
            UIManager.OpenUI<UIMainMenu>(E_UILevel.Common);
        }

        /// <summary>
        /// 获得UI根节点下的层级节点
        /// </summary>
        /// <param name="uiLevel"></param>
        /// <returns></returns>
        public RectTransform GetUILevelTrans(E_UILevel uiLevel)
        {
            if (!_uiRootDic.TryGetValue(uiLevel, out var rect))
            {
                rect = Instance.UIRootRect.Find(uiLevel.ToString()) as RectTransform;
                _uiRootDic[uiLevel] = rect;
            }
            return rect;
        }

        /// <summary>
        /// Log
        /// </summary>
        public void Log(E_Log logType, string title = null, string content = null, string color = null)
        {
            string tempStr = string.Empty;
            if (title == null || content == null)
            {
                tempStr = "<color={0}>{1}</color>";
            }
            else
            {
                tempStr = "<color={0}>{1}</color>===><color={0}>{2}</color>";
            }

            switch (logType)
            {
                case E_Log.Log:
                    Debug.Log(string.Format(tempStr, "white", title, content));
                    break;
                case E_Log.Framework:
                    Debug.Log(string.Format(tempStr, "magenta", title, content));
                    break;
                case E_Log.Proto:
                    Debug.Log(string.Format(tempStr, "#00ffff", title, content));
                    break;
                case E_Log.Error:
                    Debug.Log(string.Format(tempStr, "red", title, content));
                    break;
                case E_Log.Warring:
                    Debug.Log(string.Format(tempStr, "yellow", title, content));
                    break;
                case E_Log.Custom:
                    Debug.Log(string.Format(tempStr, color, title, content));
                    break;
            }
        }
    }

    /// <summary>
    /// UI层级
    /// </summary>
    public enum E_UILevel
    {
        Background,
        Common,
        Pop,
        Loading,
        Tips,
    }

    /// <summary>
    /// Log类型
    /// </summary>
    public enum E_Log
    {
        Log,        //普通Log
        Framework,  //框架Log
        Proto,      //联网Log
        Error,      //错误Log
        Warring,    //警告Log
        Custom,     //自定义颜色Log
    }
}