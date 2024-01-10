/*********************************************
 * BFramework
 * 游戏框架总控制器
 * 创建时间：2022/12/25 20:40:23
 *********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 游戏框架总控制器
    /// </summary>
    public partial class GameGod
    {
        /// <summary>
        /// Update回调
        /// </summary>
        public Action UpdateCallback;

        /// <summary>
        /// 退出回调回调
        /// </summary>
        public Action DisposeCallback;

        /// <summary>
        /// 全局用的加载器，基本不释放
        /// </summary>
        public LoadHelper LoadHelper { private set; get; }

        public static GameGod Instance { private set; get; }
        public PoolManager PoolManager { private set; get; }
        public HttpManager HttpManager { private set; get; }
        public SocketManager SocketManager { private set; get; }
        public UIManager UIManager { private set; get; }
        public LoadManager LoadManager { private set; get; }
        public EventManager EventManager { private set; get; }
        public TableManager TableManager { private set; get; }
        public AudioManager AudioManager { private set; get; }
        public TimerManager TimeManager { private set; get; }
        public FsmManager FsmManager { private set; get; }
        public ModuleManager ModuleManager { private set; get; }
        public RedPointManager RedPointManager { private set; get; }


        private void Awake()
        {
            Instance = this;
            _uiRootDic = new Dictionary<E_UILevel, RectTransform>();
            DontDestroyOnLoad(Instance);
            DontDestroyOnLoad(UIRoot);
            DontDestroyOnLoad(ObjPool);

            //限定60fps
            Application.targetFrameRate = 60;

            //初始化管理器
            LoadManager = new LoadManager();
            PoolManager = new PoolManager();
            HttpManager = new HttpManager();
            SocketManager = new SocketManager();
            UIManager = new UIManager();
            ModuleManager = new ModuleManager();
            EventManager = new EventManager();
            TableManager = new TableManager();
            AudioManager = new AudioManager();
            TimeManager = new TimerManager();
            FsmManager = new FsmManager();
            RedPointManager = new RedPointManager();

            //初始化加载器
            LoadHelper = LoadHelper.Create();
        }

        private void Start()
        {
            OnInit();
        }

        private void Update()
        {
#if UNITY_EDITOR
            Time.timeScale = TimeScale;
#endif

            PoolManager.OnUpdate();
            HttpManager.OnUpdate();
            SocketManager.OnUpdate();
            UIManager.OnUpdate();
            LoadManager.OnUpdate();
            ModuleManager.OnUpdate();
            EventManager.OnUpdate();
            TableManager.OnUpdate();
            AudioManager.OnUpdate();
            TimeManager.OnUpdate();
            FsmManager.OnUpdate();
            RedPointManager.OnUpdate();
            UpdateCallback?.Invoke();
        }

        private void OnApplicationQuit()
        {
            //先执行
            DisposeCallback?.Invoke();
        }

        private void OnDestroy()
        {
            //再执行
            ModuleManager.OnDispose();
            FsmManager.OnDispose();
            UIManager.OnDispose();
            PoolManager.OnDispose();
            LoadManager.OnDispose();
            EventManager.OnDispose();
            TableManager.OnDispose();
            HttpManager.OnDispose();
            SocketManager.OnDispose();
            AudioManager.OnDispose();
            RedPointManager.OnDispose();
            TimeManager.OnDispose();
        }
    }
}