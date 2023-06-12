﻿using AutoMapper;
using ProductCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.Commands
{
	public class CreateProductMapper : Profile
	{
        public CreateProductMapper()
        {
            CreateMap<CreateProductRequest, Product>();
        }
    }
}