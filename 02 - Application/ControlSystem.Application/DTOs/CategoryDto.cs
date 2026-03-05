using ControlSystem.Domain.Enums;

namespace ControlSystem.Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PurposeType { get; set; }
        public string PurposeTypeName => ((PurposeType)PurposeType).ToString();
    }
}