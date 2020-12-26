namespace OilToolsPlantform.Data.DTO
{
    public class PQLogModify : RequestBase
    {
        public string Operate { get; set; }
        public string Function { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Description { get; set; }
    }
}