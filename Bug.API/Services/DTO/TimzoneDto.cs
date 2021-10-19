using System;

namespace Bug.Entities.Dtos
{
    public class TimzoneDto
    {
        public string Id { get; set; }
        public string CountryCode { get; set; }
        public double UTCOffset { get; set; }
        public double UTCDSTOffset { get; set; }

    }
}