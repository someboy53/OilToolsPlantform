namespace OilToolsPlantform.Data.DTO
{
    public class PQWechatFanQuery : ListRequestBase
    {
        public string nickname { get; set; }
        public string city { get; set; }
        public string country
        {
            get; set;
        }
        public string province
        {
            get; set;
        }
        public string remark { get; set; }

    }
