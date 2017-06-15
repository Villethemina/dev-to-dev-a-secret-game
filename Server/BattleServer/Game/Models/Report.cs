using System;

namespace Game.Models
{

    public class Report
    {
        public Int32 Turn { get; set; }
        public Move You { get; set; }
        public Move Enemy { get; set; }
    }
}