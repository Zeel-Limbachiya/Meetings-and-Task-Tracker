using System;
using System.Collections.Generic;

namespace MeetingAndTaskTracker.Models;

public partial class Meeting
{
    public int Id { get; set; }

    public int MeetingNo { get; set; }

    public DateTime Date { get; set; }

    public string Title { get; set; } = null!;

    public int OrganizerId { get; set; }

    public string? Description { get; set; }
}
