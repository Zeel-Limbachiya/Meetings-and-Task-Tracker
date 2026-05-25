using System;
using System.Collections.Generic;

namespace MeetingAndTaskTracker.Models;

public partial class MeetingParticipant
{
    public int Id { get; set; }

    public int MeetingId { get; set; }

    public int ParticipantId { get; set; }
}
