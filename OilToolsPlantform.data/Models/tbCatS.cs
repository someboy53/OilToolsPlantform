//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OilToolsPlantform.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// tbCatS
    /// </summary>
    public partial class tbCatS
    {
        public tbCatS()
        {
            this.tbCatSPic = new HashSet<tbCatSPic>();
            this.tbTool = new HashSet<tbTool>();
        }
    
    	/// <summary>
        /// 
        /// </summary>
        public int CatSID { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public int CatFID { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string NameJP { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string NameQP { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string CreateUser { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string UpdateUser { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string Enabled { get; set; }
    
        public virtual tbCatF tbCatF { get; set; }
        public virtual ICollection<tbCatSPic> tbCatSPic { get; set; }
        public virtual ICollection<tbTool> tbTool { get; set; }
    }
}
