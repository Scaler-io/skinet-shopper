using System;

namespace Skinet.BusinessLogic.Core.Dtos
{
    public class MetaDataDto
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string LastModifieddBy { get; set; }
    }
}
