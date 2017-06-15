using System;

namespace Game.Models
{
    public class Move
    {
        public Target Target { get; set; }
        public EventType Event { get; set; }
        public String Ship { get; set; }
        public String Message { get; set; }
    }

    public enum EventType
    {
        HIT,
        MISS,
        SUNK   
    }
}