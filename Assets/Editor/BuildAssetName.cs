/*********************************************
 * BFramework
 * 资产名字生成工具
 * 创建时间：2023/04/21 17:44:36
 *********************************************/
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 资产名字生成
    /// </summary>
    public class BuildAssetName : Editor
    {
        /// <summary>
        /// 资产名字代码磁盘路径
        /// </summary>
        private static string _assetNamesScriptPath = Application.dataPath + "/Resources/Scripts/Define/AssetName.cs";
        private static string _assetNamesJsonPath = Application.dataPath + "/Resources/Scripts/Define/AssetName.json";
        private static string _assetNamesPath = "Scripts/Define/AssetName";

        [MenuItem("BFramework/Build Asset Name", false, 1)]
        public static void BuildAssetNamesScript()
        {
            string temp = @"/*********************************************
 * 自动生成代码，禁止手动修改文件
 * 脚本名：AssetName.cs
 * 创建时间：#Time
 *********************************************/

namespace GameData
{
    /// <summary>
    /// 快捷获取资产名
    /// </summary>
    public static class AssetName
    {#AssetName
    }
}
";


            //2024.01.10 Add 用于存储一份路径表
            var _assetPathDic = new Dictionary<string, string>();

            string assetNames = string.Empty;

            //2024.01.10 Add 路径表的位置
            assetNames += $"\r\n        public const string AssetNamesJson = \"{_assetNamesPath}\";";

            var needSearchPathList = Directory.GetDirectories(Application.dataPath + "/Resources");
            //循环全部需要打包的地址
            for (int i = 0,count = needSearchPathList.Length; i < count; i++)
            {
                var pathDir = new DirectoryInfo(needSearchPathList[i]);
                //不生成配表、脚本、字体、动画
                if (pathDir.Name == "Table" || pathDir.Name == "Scripts")
                {
                    continue;
                }
                //循环目录下的全部文件
                foreach (var item in pathDir.GetFileSystemInfos("*.*", SearchOption.AllDirectories))
                {
                    var fileInfo = item as FileInfo;
                    if (fileInfo != null)
                    {
                        var file = new FileInfo(fileInfo.FullName);
                        //不是meta和图集文件
                        if (file.Extension.ToLower() != ".meta" && file.Extension.ToLower() != ".spriteatlas" 
                            && file.Extension.ToLower() != ".anim" && file.Extension.ToLower() != ".controller")
                        {
                            var prefix = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fileInfo.Extension.Replace(".", ""));
                            var fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                            var name = fileName.Replace(".", "_");
                            name = fileName.Replace("-", "_");
                            //
                            assetNames += $"\r\n        public const string {prefix}_{name} = \"{fileName}\";";

                            //2024.01.10 Add 添加对应的名称和路径
                            var path = fileInfo.FullName.Substring(Application.dataPath.Length + 11).Replace("\\", "/");
                            path = path.Substring(0, path.Length - fileInfo.Extension.Length);
                            _assetPathDic.Add(fileName, path);
                        }
                    }
                }
            }

            //2024.01.10 Add 存储路径表
            using (var pathJson = File.CreateText(_assetNamesJsonPath))
            {
                pathJson.Write(_assetPathDic.ToJson());
            }

            //导出文件 替换文本
            var scripts = File.CreateText(_assetNamesScriptPath);
            temp = temp.Replace("#Time", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            temp = temp.Replace("#AssetName", assetNames);
            scripts.Write(temp);
            scripts.Close();
            Debug.Log("资产名字代码生成完毕!");

            //回收资源
            System.GC.Collect();
            //刷新编辑器
            AssetDatabase.Refresh();
        }
    }
}
