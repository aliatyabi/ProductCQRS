﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Domain.Entities
{
	[Microsoft.EntityFrameworkCore.Index(nameof(ProduceDate), nameof(ManufactureEmail), IsUnique = true)]
	public class Product
    {
        public int Id { get; set; }

        public DateTime ProduceDate { get; set; }      

		public string? Name { get; set; }

		[Column(TypeName = "varchar(11)")]
		public string? ManufacturePhone { get; set; }

        public string? ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }
    }
}
