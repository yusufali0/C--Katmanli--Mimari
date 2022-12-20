﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAB.ToDoAppNTier.Dtos.Interfaces;

namespace YAB.ToDoAppNTier.Dtos.WorkDtos
{
    public class WorkListDto:IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
