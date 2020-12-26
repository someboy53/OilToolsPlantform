namespace OilToolsPlantform.Data.DTO
{
    public class PQRoleModify : RequestBase
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string IsDel { get; set; }
        public PQRoleModify()
        {
            this.IsDel = "0";
        }
    }
}