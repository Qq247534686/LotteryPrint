using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Model
{
    public class SuanJiangClass
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string BuyTitckID { get; set; }
        /// <summary>
        /// 玩法
        /// </summary>
        public string PlayType { get; set; }
        /// <summary>
        /// 倍数
        /// </summary>
        public string Multiple { get; set; }
        /// <summary>
        /// 投注内容
        /// </summary>
        public string TouZhu { get; set; }
        /// <summary>
        /// 投注赔率
        /// </summary>
        public string TouZhuValue { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


    }
}
