/*********************************************
 * BFramework
 * UI管理器
 * 创建时间：2023/01/08 20:40:23
 *********************************************/
using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : ManagerBase
    {
        /// <summary>
        /// 已打开的UI字典
        /// </summary>
        private Dictionary<string, UIBase> _uiBaseDic;

        public override void OnInit()
        {
            _uiBaseDic = new Dictionary<string, UIBase>();
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        public void OpenUI<T>(E_UILevel uiLevel, params object[] args) where T : UIBase,new()
        {
            var uiName = typeof(T).Name;
            OpenUI<T>(uiName,uiLevel, args);
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        public void OpenUI<T>(string uiName, E_UILevel uiLevel, params object[] args) where T : UIBase, new()
        {
            //已打开 直接显示
            if (_uiBaseDic.TryGetValue(uiName, out var uiBase))
            {
                uiBase.gameObject.SetActive(true);
                uiBase.OnShow(args);
                return;
            }

            uiBase = new T();
            var obj = GameGod.Instance.LoadManager.LoadSyncByObjName<GameObject>(uiName);
            var uiTrans = GameGod.Instance.GetUILevelTrans(uiLevel);
            _uiBaseDic[uiName] = uiBase;
            uiBase.uiName = uiName;
            uiBase.gameObject = UnityEngine.Object.Instantiate(obj, uiTrans);
            uiBase.LoadHelper = LoadHelper.Create();
            uiBase.OnCreate();
            uiBase.OnInit();
            uiBase.OnShow(args);
        }

        /// <summary>
        /// 获取UI
        /// </summary>
        public UIBase GetUI<T>() where T : UIBase
        {
            var uiName = typeof(T).Name;
            return GetUI(uiName);
        }

        /// <summary>
        /// 获取UI
        /// </summary>
        public UIBase GetUI(string uiName)
        {
            if (!_uiBaseDic.TryGetValue(uiName, out var uiBase))
            {
                return null;
            }
            return uiBase;
        }

        /// <summary>
        /// 隐藏UI
        /// </summary>
        public void HideUI<T>() where T : UIBase
        {
            var uiName = typeof(T).Name;
            HideUI(uiName);
        }

        /// <summary>
        /// 隐藏UI
        /// </summary>
        public void HideUI(string uiName)
        {
            //已打开 直接关闭
            if (_uiBaseDic.TryGetValue(uiName, out var uiBase))
            {
                uiBase.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        public void CloseUI<T>() where T : UIBase
        {
            var uiName = typeof(T).Name;
            CloseUI(uiName);
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        public void CloseUI(string uiName)
        {
            //已打开 直接关闭
            if (_uiBaseDic.TryGetValue(uiName, out var uiBase))
            {
                uiBase.OnDispose();
                Object.Destroy(uiBase.gameObject);
                uiBase = null;
                _uiBaseDic.Remove(uiName);
                GameGod.Instance.LoadManager.UnloadAsset(uiName + ".prefab");
            }
        }

        /// <summary>
        /// 关闭全部UI
        /// </summary>
        public void CloseAll()
        {
            foreach (var item in _uiBaseDic)
            {
                item.Value.OnDispose();
                Object.Destroy(item.Value.gameObject);
                GameGod.Instance.LoadManager.UnloadAsset(item.Key + ".prefab");
            }
            _uiBaseDic.Clear();
        }

        public override void OnUpdate() { }
        public override void OnDispose() 
        {
            CloseAll();
            _uiBaseDic = null;
        }
    }
}