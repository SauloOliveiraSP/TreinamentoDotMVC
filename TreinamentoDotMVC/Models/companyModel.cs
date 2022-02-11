using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace CompanyModel.Models
{
    public class DataAccess
    {   // Objeto Connection para obter acesso ao SQL Server
       static public SqlConnection Connection = new SqlConnection();

        // Objeto SqlCommand para executar os com
       static public SqlCommand Command = new SqlCommand();

    // Objeto SqlParameter para adicionar os parâmetros necessarios nas consultas
       static public SqlParameter Parameters = new SqlParameter();

        public SqlConnection connection()
        {
             try
            {
                // Obtemos os dados da conexão existentes no WebConfig
                string dadosConexao = "SERVER=SBD\\SQL2016;UID=sa;PWD=spypreto;DATABASE=volpepwiteste";
                // Instanciando o objeto SqlConnection

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(dadosConexao);
                Connection = new SqlConnection(builder.ConnectionString);
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

        public void Open()
        {
            Connection.Open();
        } 

        public void Close()
        {
            Connection.Close();
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

        public void removeParameters (string paranName)
        {
            if(Command.Parameters.Contains(paranName))
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


