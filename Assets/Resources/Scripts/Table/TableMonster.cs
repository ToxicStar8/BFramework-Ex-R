/*********************************************
 * 自动生成代码，禁止手动修改文件
 * Excel表名：G_怪物表.xlsx.temp
 * 修改时间：2023/12/20 04:40:31
 *********************************************/

using Framework;

namespace GameData
{
    public partial class TableMonster : TableBase
    {        
        /// <summary>
        /// 
        /// </summary>
        public override int Id { protected set; get; }
        
        /// <summary>
        /// 英文名
        /// </summary>
        public string En_Name { private set; get; }
        
        /// <summary>
        /// 中文名
        /// </summary>
        public string Cn_Name { private set; get; }
        
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp { private set; get; }
        
        /// <summary>
        /// 速度
        /// </summary>
        public float Speed { private set; get; }
        
        /// <summary>
        /// 伤害
        /// </summary>
        public float Damage { private set; get; }
        
        /// <summary>
        /// 掉落经验
        /// </summary>
        public int Exp { private set; get; }
        
        /// <summary>
        /// 怪物预制体名
        /// </summary>
        public string Prefab { private set; get; }

        public override void OnInit(string[] group, string dataStrArr)
        {
            var data = dataStrArr.Split('^');
            for (int i = 0,length = group.Length; i < length; i++)
            {
                switch (group[i])
                {
                    case "id":
                        Id = data[i].ToInt();;
                        break;

                    case "en_name":
                        En_Name = data[i];;
                        break;

                    case "cn_name":
                        Cn_Name = data[i];;
                        break;

                    case "hp":
                        Hp = data[i].ToInt();;
                        break;

                    case "speed":
                        Speed = data[i].ToFloat();;
                        break;

                    case "damage":
                        Damage = data[i].ToFloat();;
                        break;

                    case "exp":
                        Exp = data[i].ToInt();;
                        break;

                    case "prefab":
                        Prefab = data[i];;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
