using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    [Serializable]
    public class QueryBase
    {
        /// <summary>
        /// 1可编辑0不可编辑
        /// 针对所有数据
        /// 新增,修改,提交,启禁用,删除
        /// </summary>
        public string CanEdit { get; set; }

        /// <summary>
        /// 1可审核0不可审核
        /// 针对审核数据
        /// 审核通过,审核不通过
        /// </summary>
        public string CanAudit { get; set; }

        /// <summary>
        /// 1可撤消0不可撤消
        /// 针对审核数据
        /// 撤消
        /// </summary>
        public string CanCancel { get; set; }
    }

}
