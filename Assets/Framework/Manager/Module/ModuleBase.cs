﻿/*********************************************
 * BFramework
 * 脚本名：ModuleBase.cs
 * 创建时间：2023/04/06 11:45:09
 *********************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 数据操作基类
    /// </summary>
    public abstract class ModuleBase : GameBase
    {
        protected Coroutine Coroutine;

        /// <summary>
        /// 初始化Module，只有Init时会执行，Load时不执行
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// Load存档时执行，只有Load时会执行，Init时不执行
        /// </summary>
        public abstract void OnLoad();

        #region Event
        private List<ushort> _eventList;

        protected override void AddEventListener(ushort eventNo, Action<object[]> callBack)
        {
            if (_eventList == null)
            {
                _eventList = new List<ushort>();
            }
            _eventList.Add(eventNo);
            base.AddEventListener(eventNo, callBack);
        }
        #endregion

        #region Update
        private Action _update;
        protected void RegisterUpdate(Action updateCallback)
        {
            if (_update != null)
            {
                GameGod.Instance.Log(E_Log.Error, GetType().Name , "Update重复注册！");
                return;
            }

            _update = updateCallback;
            GameGod.Instance.UpdateCallback += _update;
        }
        #endregion

        /// <summary>
        /// 注销前调用
        /// </summary>
        public abstract void OnBeforeDispose();

        /// <summary>
        /// 关闭Module
        /// </summary>
        public void OnDispose()
        {
            //注销前执行
            OnBeforeDispose();

            //关闭前移除全部Update回调
            if (_update != null)
            {
                GameGod.Instance.UpdateCallback -= _update;
                _update = null;
            }

            //关闭前移除全部注册事件
            if (_eventList != null)
            {
                for (int i = 0, count = _eventList.Count; i < count; i++)
                {
                    RemoveEventListener(_eventList[i]);
                }
                _eventList.Clear();
                _eventList = null;
            }

            //移除协程
            if (Coroutine != null)
            {
                GameGod.Instance.StopCoroutine(Coroutine);
            }
        }
    }
}
