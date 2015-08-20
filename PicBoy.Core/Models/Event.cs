﻿using System;

namespace PicBoy.Core.Models
{
    class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }
    }
}