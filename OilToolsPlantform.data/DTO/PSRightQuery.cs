using System.Collections.Generic;
namespace OilToolsPlantform.Data.DTO
{
    public class PSRightQuery:ListResponseBase
    {
        public List<LRight> data { get; set; }

        public PSRightQuery()
        {
            this.data = new List<LRight>();
        }
    }
}