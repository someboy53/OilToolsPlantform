namespace OilToolsPlantform.Data.DTO
{
    public class PQRegist:RequestBase
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string passwd { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string name { get; set; }
        public string phone { get; set; }
    }
}