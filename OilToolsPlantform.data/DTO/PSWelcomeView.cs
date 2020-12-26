using System.Collections.Generic;

namespace OilToolsPlantform.Data.DTO
{
    public class PSWelcomeView:ViewResponseBase
    {
        public int FanCount { get; set; }
        public int ToolCount { get; set; }
        public List<LCountAna> data { get; set; }

        public PSWelcomeView() {
            this.data = new List<LCountAna>();
        }
    }
}