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
    /// tbCatF
    /// </summary>
    public partial class tbCatF
    {
        public tbCatF()
        {
            this.tbCatS = new HashSet<tbCatS>();
            this.tbCatFPic = new HashSet<tbCatFPic>();
        }
    
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
    
        public virtual ICollection<tbCatS> tbCatS { get; set; }
        public virtual ICollection<tbCatFPic> tbCatFPic { get; set; }
    }
}
