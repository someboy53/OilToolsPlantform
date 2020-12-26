using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    [Serializable]
    public class ViewResponseBase:ResponseBase
    {
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 浏览数量
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// 收藏数量
        /// </summary>
        public int FavCount { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// 点赞分数
        /// </summary>
        public decimal LikeScore { get; set; }
        /// <summary>
        /// 点赞平均分数
        /// </summary>
        public decimal LikeScore1 { get; set; }
    }
}
