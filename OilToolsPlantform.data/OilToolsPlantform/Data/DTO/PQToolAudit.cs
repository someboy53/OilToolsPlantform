namespace OilToolsPlantform.Data.DTO
{
    public class PQToolAudit: RequestBase
    {
        public int ToolID { get; set; }

        public string IsPass { get; set; }
        public string PassDesc { get; set; }

        public PQToolAudit()
        {
            this.IsPass = "0";
        }
    }
}