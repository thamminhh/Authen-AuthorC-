using System;
using System.Collections.Generic;

namespace JwtWebAPITutorial.Entities
{
    public partial class CarTrim
    {
        public int? Id { get; set; }
        public int? CarModelId { get; set; }
        public int? CarSeriesId { get; set; }
        public string? Name { get; set; }
        public int? StartProductYear { get; set; }
        public int? EndProductYear { get; set; }
    }
}
