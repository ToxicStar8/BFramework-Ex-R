/*********************************************
 * BFramework
 * 加载管理器
 * 创建时间：2023/01/08 20:40:23
 *********************************************/
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;

namespace Framework
{
    public class LoadManager : ManagerBase
    {
        /// <summary>
        /// objName,objPath
        /// </summary>
        private Dictionary<string, string> _objPathDic;

        /// <summary>
        /// 已经加载的资源字典
        /// </summary>
        private Dictionary<string, Object> _objLoadedDic;

        public override void OnInit()
        {
            //文件寻址路径的文件
            var textAsset = Resources.Load<TextAsset>(GameData.AssetName.AssetNamesJson);
            _objPathDic = JsonMapper.ToObject<Dictionary<string, string>>(textAsset.text);

            _objLoadedDic = new Dictionary<string, Object>();
        }

        /// <summary>
        /// 同步加载资源 带后缀
        /// </summary>
        public T LoadSyncByObjName<T>(string objName) where T : Object
        {
            var obj = LoadSyncByObjName(objName);
            return obj as T;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public Object LoadSyncByObjName(string objName)
        {
            if (!_objLoadedDic.TryGetValue(objName, out var obj))
            {
                if(!_objPathDic.TryGetValue(objName, out var objPath))
                {
                    GameGod.Instance.Log(E_Log.Error, "未找到资源路径", objName);
                    return null;
                }
                
                obj = Resources.Load<Object>(objPath);
                _objLoadedDic.Add(objName, obj);
            }
            return obj;
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        public Sprite GetSprite(string atlasName,string spriteName)
        {
            Sprite sp = null;
            SpriteAtlas atlas = LoadSyncByObjName<SpriteAtlas>(atlasName);
            if (atlas != null)
            {
                sp = atlas.GetSprite(spriteName);
            }
            return sp;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public void LoadASync(string objName, System.Action Callback)
        {

        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        public void UnloadAsset(string objName)
        {
            if(_objLoadedDic.TryGetValue(objName,out var obj))
            {
                //卸载Asset
                obj = null;
                _objLoadedDic.Remove(objName);
                Resources.UnloadAsset(obj);
            }
        }

        public override void OnUpdate() { }
        public override void OnDispose()
        {
            _objLoadedDic.Clear();
            _objLoadedDic = null;
        }
    }
}
