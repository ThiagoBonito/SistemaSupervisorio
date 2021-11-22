using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace SistemaSupervisorio
{
    class DAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDSistema.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;

        public static void conecta()
        {
            conn.Open();
        }
        public static void desconecta()
        {
            conn.Close();
        }
        public static void Inserir(Dado umdado)
        {
            conecta();
            String aux = "insert into Tabela1(dia,mes,ano,dado, hora) values(@dia,@mes,@ano,@dado, @hora)";
            strSQL = new OleDbCommand(aux, conn);
            strSQL.Parameters.Add("@dia", OleDbType.VarChar).Value = umdado.dia;
            strSQL.Parameters.Add("@mes", OleDbType.VarChar).Value = umdado.mes;
            strSQL.Parameters.Add("@ano", OleDbType.VarChar).Value = umdado.ano;          
            strSQL.Parameters.Add("@dado", OleDbType.VarChar).Value = umdado.dado;
            strSQL.Parameters.Add("@hora", OleDbType.VarChar).Value = umdado.hora;
            strSQL.ExecuteNonQuery();
            desconecta();
        }
    }
}
