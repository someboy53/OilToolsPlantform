using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    [Serializable]
    public class CountQueryBase
    {
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 通过评论数量
        /// </summary>
        public int AuditCommentCount { get; set; }
        /// <summary>
        /// 浏览数量
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// 收藏数量
        /// </summary>
        public int FavoriteCount { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// 预定和购买未付款前的数量,同时也是借用(包括冻结)数量
        /// </summary>
        public int PreOrderCount { get; set; }
        /// <summary>
        /// 付款后的数量,同时也是实际借用数量
        /// </summary>
        public int BuyCount { get; set; }
    }

}
