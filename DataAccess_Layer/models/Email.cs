﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.models
{
	public class Email
	{
		public string Subject { get; set; }

		public string Body { get; set; }
		public string Recipient { get; set; }
	}

}
