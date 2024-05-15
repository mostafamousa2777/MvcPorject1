﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.models
{
	public class AppUser :IdentityUser
	{
		public string Fname { get; set; }
		public string LName { get; set; }
		public bool Agree { get; set; }
	}
}
