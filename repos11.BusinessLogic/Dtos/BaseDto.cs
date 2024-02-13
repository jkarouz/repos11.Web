using Newtonsoft.Json;
using System;
using System.Web.Script.Serialization;

namespace repos11.BusinessLogic.Dtos
{
    //[JsonIgnore] => for JSON.NET
    //[ScriptIgnore] => for JavaScriptSerializer

    public class BaseDto : IDto
    {
        public long Id { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        public DateTime CreatedDate { get; set; }

        public string CreatedDateStr
        {
            get
            {
                return CreatedDate.ToString("yyyy-MM-dd");
            }
        }

        public int? UpdatedBy { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        public DateTime? UpdatedDate { get; set; }

        public string UpdatedDateStr
        {
            get
            {
                return (UpdatedDate != null) ? UpdatedDate.Value.ToString("yyyy-MM-dd") : null;
            }
        }
    }
}
