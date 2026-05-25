using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingAndTaskTracker.Models;

public partial class User
{
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

    public int? EmpId { get; set; }

    public string Role { get; set; } = null!;

    public virtual Employee? Emp { get; set; }
}
