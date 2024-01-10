/*********************************************
 * BFramework
 * 游戏常量
 * 创建时间：2023/04/03 14:13:23
 *********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameData
{
    /// <summary>
    /// 游戏常量
    /// </summary>
    public static class ConstDefine
    {
        public const string MoveSyncKey = "MoveSync";                       //移动同步
        public const string CreateMonsterTimeKey = "CreateMonsterTimeKey";  //刷怪定时器Key

        public const int RoomMaxPeople = 4;                                 //游戏最大人数上限
    }
}
