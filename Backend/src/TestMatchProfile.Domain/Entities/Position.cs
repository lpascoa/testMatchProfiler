﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestMatchProfile.Domain.Common;

namespace TestMatchProfile.Domain.Entities
{
    public class Position : AuditableBaseEntity
    {
        public Guid Id { get; set; }
        public string PositionTitle { get; set; }
        public string PositionNumber { get; set; }
        public string PositionDescription { get; set; }
        public string PostionArea { get; set; }
        public string PostionType { get; set; }
        public decimal PositionSalary { get; set; }
    }
}