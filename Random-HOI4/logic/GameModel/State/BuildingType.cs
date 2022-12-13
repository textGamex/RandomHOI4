using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_HOI4.Logic.GameModel.State
{
    internal enum BuildingType : byte
    {
        /// <summary>
        /// 基础设施
        /// </summary>
        INFRASTRUCTURE,
        /// <summary>
        /// 民用工厂
        /// </summary>
        INDUSTRIAL_COMPLEX,
        /// <summary>
        /// 空军基地
        /// </summary>
        AIR_BASE,
        /// <summary>
        /// 防空炮
        /// </summary>
        ANTI_AIR_BUILDING,
        /// <summary>
        /// 军事工厂
        /// </summary>
        ARMS_FACTORY,
        /// <summary>
        /// 雷达站
        /// </summary>
        RADAR_STATION,
        /// <summary>
        /// 船坞
        /// </summary>
        DOCKYARD,
        /// <summary>
        /// 合成炼油厂
        /// </summary>
        SYNTHETIC_REFINERY,
        /// <summary>
        /// 燃料筒仓
        /// </summary>
        FUEL_SILO,
        /// <summary>
        /// 火箭发射场
        /// </summary>
        ROCKET_SITE,
        /// <summary>
        /// 核反应堆
        /// </summary>
        NUCLEAR_REACTOR
    }
}
