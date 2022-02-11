using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Configuration;
using TreinamentoDotMVC.Models;

namespace TreinamentoDotMVC.Data
{
    public class DataAccess
    {   // Objeto Connection para obter acesso ao SQL Server
        public static SqlConnection Connection = new SqlConnection();

        // Objeto SqlCommand para executar os com
        public static SqlCommand Command = new SqlCommand();

        // Objeto SqlParameter para adicionar os parâmetros necessarios nas consultas
        public static SqlParameter Parameters = new SqlParameter();

        public static SqlConnection connection()
        {
            try
            {
                // Obtemos os dados da conexão existentes no WebConfig
                string dadosConexao = "SERVER = sbd\\sql2016;UID = sa;PWD = spypreto;DATABASE = VolpePwiTeste";
                // Instanciando o objeto SqlConnection
                Connection = new SqlConnection(dadosConexao);
                //Verifica se a conexão esta fechada.
                if (Connection.State == ConnectionState.Closed)
                {
                    //Abre a conexão.
                    Connection.Open();
                }
                //Retorna o sqlconnection.
                return Connection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public void AddParameters(string name, SqlDbType type, int size, object value)
        {
            Parameters = new SqlParameter();
            Parameters.ParameterName = name;
            Parameters.SqlDbType = type;
            Parameters.Size = size;
            Parameters.Value = value;

            //add parametro no comando Sql
            Command.Parameters.Add(Parameters);
        }

        public void AdicionarParametro(string nome, SqlDbType tipo, object valor)
        {
            // Cria a instância do Parâmetro e adiciona os valores
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nome;
            parametro.SqlDbType = tipo;
            parametro.Value = valor;
            // Adiciona ao comando SQL o parâmetro
            Command.Parameters.Add(parametro);
        }

        public void removeParameters(string paranName)
        {
            if (Command.Parameters.Contains(paranName))
                Command.Parameters.Remove(paranName);
        }

        public void cleanParameters()
        {
            Command.Parameters.Clear();
        }

        public DataTable ExecConsult(string sql)
        {
            try
            {
                //Pega a conexão com a base SQl 
                Command.Connection = connection();

                //Adiciona a instrução
                Command.CommandText = sql;

                //Executa a query
                Command.ExecuteScalar();

                //Ler os dados tabela
                IDataReader dtreader = Command.ExecuteReader();
                DataTable dtresult = new DataTable();
                dtresult.Load(dtreader);

                //fecha conexão
                Connection.Close();

                return dtresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecAtt(string sql)
        {
            try
            {
                Command.Connection = connection();
                Command.CommandText = sql;

                //exec query
                int result = Command.ExecuteNonQuery();
                Connection.Close();
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        //CRUD

        // CREATE - (VÁLIDAR DS_UF POIS NÃO ACEITA MAIS DE DOIS CARACTERES), (TG_NAONEGATIVAR E FK_CARTCOB SÃO OBRIGATÓRIOS).
        UserViewModel user = new UserViewModel();

        public bool InsertUsuario(UserViewModel user)
        {
            cleanParameters();

            string SQL = @"INSERT INTO TB_CADUNICO
                    (DS_FANTASIA, DS_RAZAO, DS_ENDERECO, DS_BAIRRO, DS_CIDADE, DS_UF, DS_CEP, DS_FONE, DS_EMAIL, DS_OBS, DS_HOMEPAGE, TG_NAONEGATIVAR, FK_CARTCOB)
                VALUES
                    (@DS_FANTASIA, @DS_RAZAO, @DS_ENDERECO, @DS_BAIRRO, @DS_CIDADE, @DS_UF, @DS_CEP, @DS_FONE, @DS_EMAIL, @DS_OBS, @DS_HOMEPAGE, 1, 'JU')";

            AdicionarParametro("DS_FANTASIA", SqlDbType.VarChar, user.Fantasia);
            AdicionarParametro("DS_RAZAO", SqlDbType.VarChar, user.Razao);
            AdicionarParametro("DS_ENDERECO", SqlDbType.VarChar, user.Endereco);
            AdicionarParametro("DS_BAIRRO", SqlDbType.VarChar, user.Bairro);
            AdicionarParametro("DS_CIDADE", SqlDbType.VarChar, user.Cidade);
            AdicionarParametro("DS_UF", SqlDbType.VarChar, user.Uf);
            AdicionarParametro("DS_CEP", SqlDbType.VarChar, user.Cep);
            AdicionarParametro("DS_FONE", SqlDbType.VarChar, user.Telefone);
            AdicionarParametro("DS_EMAIL", SqlDbType.VarChar, user.Email);
            AdicionarParametro("DS_OBS", SqlDbType.VarChar, user.Obs);
            AdicionarParametro("DS_HOMEPAGE", SqlDbType.VarChar, user.Homepage);

            return (ExecAtt(SQL) > 0);
        }

        //READ - OK
        public DataTable GetUsuario(int id)
        {
            cleanParameters();
            string SQL = @"SELECT 
	            PK_ID               AS CODIGO
	            ,DS_FANTASIA        AS FANTASIA
	            ,DS_RAZAO           AS RAZAO
	            ,DS_ENDERECO        AS ENDERECO
	            ,DS_BAIRRO          AS BAIRRO
	            ,DS_CIDADE          AS CIDADE
	            ,DS_UF              AS UF
	            ,DS_CEP             AS CEP
	            ,DS_FONE            AS TELEFONE
	            ,DS_EMAIL           AS EMAIL
	            ,DS_OBS             AS OBS
	            ,DS_HOMEPAGE        AS HOMEPAGE
            FROM TB_CADUNICO WHERE PK_ID = @id";

            AdicionarParametro("@id", SqlDbType.Int, id);

            return ExecConsult(SQL);
        }

        // UPDATE - (VÁLIDAR DS_UF POIS NÃO ACEITA MAIS DE DOIS CARACTERES)
        public bool AlterarUsuario(UserViewModel user)
        {
            cleanParameters();

            string SQL = @"UPDATE TB_CADUNICO SET 
                DS_FANTASIA = @DS_FANTASIA,
                DS_RAZAO    = @DS_RAZAO,
                DS_ENDERECO = @DS_ENDERECO,
                DS_BAIRRO   = @DS_BAIRRO,
                DS_CIDADE   = @DS_CIDADE,
                DS_UF       = @DS_UF,
                DS_CEP      = @DS_CEP,
                DS_FONE     = @DS_FONE,
                DS_EMAIL    = @DS_EMAIL,
                DS_OBS      = @DS_OBS, 
                DS_HOMEPAGE = @DS_HOMEPAGE
                WHERE PK_ID = @PK_ID";

            AdicionarParametro("PK_ID", SqlDbType.Int, user.Codigo);
            AdicionarParametro("DS_FANTASIA", SqlDbType.VarChar, user.Fantasia);
            AdicionarParametro("DS_RAZAO", SqlDbType.VarChar, user.Razao);
            AdicionarParametro("DS_ENDERECO", SqlDbType.VarChar, user.Endereco);
            AdicionarParametro("DS_BAIRRO", SqlDbType.VarChar, user.Bairro);
            AdicionarParametro("DS_CIDADE", SqlDbType.VarChar, user.Cidade);
            AdicionarParametro("DS_UF", SqlDbType.VarChar, user.Uf);
            AdicionarParametro("DS_CEP", SqlDbType.VarChar, user.Cep);
            AdicionarParametro("DS_FONE", SqlDbType.VarChar, user.Telefone);
            AdicionarParametro("DS_EMAIL", SqlDbType.VarChar, user.Email);
            AdicionarParametro("DS_OBS", SqlDbType.VarChar, user.Obs);
            AdicionarParametro("DS_HOMEPAGE", SqlDbType.VarChar, user.Homepage);

            if (ExecAtt(SQL) > 0);
                return true;

            return false;
        }

        // DELETE
        public bool DeleteUsuario(int id)
        {
            cleanParameters();

            string SQL = @"DELETE FROM TB_CADUNICO WHERE PK_ID = @PK_ID";
            AdicionarParametro("@PK_ID", SqlDbType.Int, id);

            if (ExecAtt(SQL) > 0)
                return true;

            return false;
        }

        //public DataTable GetFantasy(string name)
        //{
        //    cleanParameters()

        //        string SQL = @"select  
        //        PK_ID               AS CODIGO
	       //     ,DS_FANTASIA        AS FANTASIA
	       //     ,DS_RAZAO           AS RAZAO
	       //     ,DS_ENDERECO        AS ENDERECO
	       //     ,DS_BAIRRO          AS BAIRRO
	       //     ,DS_CIDADE          AS CIDADE
	       //     ,DS_UF              AS UF
	       //     ,DS_CEP             AS CEP
	       //     ,DS_FONE            AS TELEFONE
	       //     ,DS_EMAIL           AS EMAIL
	       //     ,DS_OBS             AS OBS
	       //     ,DS_HOMEPAGE        AS HOMEPAGE 

        //            from tb_cadunico where ds_fantasia LIKE '@fantasia%'";
        //    AdicionarParametro("DS_FANTASIA", SqlDbType.Char, name)

        //        return ExecConsult(SQL);
        //}
    }
}


