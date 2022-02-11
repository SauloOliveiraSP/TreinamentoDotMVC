
﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreinamentoDotMVC.Models
{	[Table("Usuarios")]
	public class UserViewModel
	{
		public int Codigo { get; set; }
		public string Fantasia { get; set; }
		public string Razao { get; set; }
		public string Endereco { get; set; }
		public string Bairro { get; set; }
		public string Cidade { get; set; }
		public string Uf { get; set; }
		public string Cep { get; set; }
		public string Fundacao { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }
		public string Homepage { get; set; }
		public string Obs { get; set; }
	}
}

