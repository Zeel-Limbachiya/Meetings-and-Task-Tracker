using System.ComponentModel.DataAnnotations;

namespace MeetingAndTaskTracker.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public int DepartmentId { get; set; }
    }
}
