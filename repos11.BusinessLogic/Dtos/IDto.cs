using System;

namespace repos11.BusinessLogic.Dtos
{
    public interface IDto
    {
        long Id { get; set; }
        bool IsActive { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
