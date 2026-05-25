using System;
using System.Collections.Generic;

namespace MeetingAndTaskTracker.Models;

public partial class ActionItem
{
    public int Id { get; set; }

    public int MeetingId { get; set; }

    public string AssignedTo { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? Priority { get; set; }

    public string Status { get; set; } = null!;

    public string? Remarks { get; set; }
}
