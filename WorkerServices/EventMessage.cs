using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerServices
{
    public class EventMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
