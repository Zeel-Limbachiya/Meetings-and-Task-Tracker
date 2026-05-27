using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingAndTaskTracker.Models;

public partial class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Name is required")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
