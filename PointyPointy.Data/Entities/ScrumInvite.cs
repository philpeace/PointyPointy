﻿using System;

namespace PointyPointy.Data.Entities
{
    public class ScrumInvite
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Created { get; set; }
    }
}