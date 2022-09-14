﻿using System.ComponentModel.DataAnnotations;

namespace BoatRazorLibrary.Models;

public class DateSelector
{
    [Range(typeof(DateTime), "1/1/2022 12:00:00 PM", "1/1/2030 12:00:00 PM", ErrorMessage = "Not Allowed Date Before 2022 - After 2030")]
    public DateTime? Timestamp { get; set; }

    public bool disabled { get; set; } = false;

    public string ToString(DateTime? timestamp)
    {
        if (timestamp.HasValue)
            return $"{timestamp.Value.ToShortDateString()} {timestamp.Value.ToShortTimeString()}";

        return null;
    }
}
