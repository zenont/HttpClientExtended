using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientExtensions.Abstractions.Test
{
    public class FakePayload
    {
        public int Id { get; set; }

        public Guid Token { get; set; }

        public DateTime NonNullDateTime { get; set; }

        public DateTime? NullableDateTime { get; set; }

        public string Note { get; set; }
    }
}
