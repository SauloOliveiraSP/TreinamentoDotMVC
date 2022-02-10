
﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreinamentoDotMVC.Models
{	[Table("Funcionarios")]
	public class UserViewModel
	{
		public int Codigo { get; set; }
		public string Pessoa { get; set; }
		public string Cpf { get; set; }
		public string Fantasia { get; set; }
		public string Nome { get; set; }
		public string Cep { get; set; }
		public string Endereco { get; set; }
		public string Cidade { get; set; }
		public string Uf { get; set; }
		public string Pais { get; set; }
		public string Bairro { get; set; }
		public string Fundacao { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }
		public string Homepage { get; set; }
		public string Obs { get; set; }
		public string InscricaoEstadual { get; set; }
		public string InscricaoMunicipal { get; set; }
	}
}

