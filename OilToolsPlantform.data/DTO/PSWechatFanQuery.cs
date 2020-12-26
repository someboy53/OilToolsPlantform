using System.Collections.Generic;
namespace OilToolsPlantform.Data.DTO
{
    public class PSWechatFanQuery:ListResponseBase
    {
        public List<DTO.LWechatFanQuery> data = new List<LWechatFanQuery>();
    }
}