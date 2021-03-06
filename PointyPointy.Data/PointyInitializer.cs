﻿using System.Data.Entity;
using PointyPointy.Data.Contexts;

namespace PointyPointy.Data
{
    public class PointyInitializer : IDatabaseInitializer<PointyContext>
    {
        public void InitializeDatabase(PointyContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}