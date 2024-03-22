using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cepdi_Prueba.Models;


namespace Cepdi_Prueba.Models
{
    public class BaseDatosMedicamento
    {
        
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

       
        public clsSalida AltaMedicamento(clsMedicamento Medicamento)
        {
            clsSalida Salida = new clsSalida();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand com = new SqlCommand("sp_alta_Medicamento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nombre", Medicamento.Nombre);
                com.Parameters.AddWithValue("@Concentracion", Medicamento.Concentracion);
                com.Parameters.AddWithValue("@Precio", Medicamento.Precio);
                com.Parameters.AddWithValue("@Presentacion", Medicamento.Presentacion);
                com.Parameters.AddWithValue("@Habilitado", Medicamento.Habilitado);
                com.Parameters.AddWithValue("@Acciones", Medicamento.Acciones);
                com.Parameters.AddWithValue("@IdFormaFarmaucetica", Medicamento.IdFormaFarmaucetica);
                com.Parameters.Add("@Exito", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;

                con.Open();

                com.ExecuteNonQuery();

                Salida.registrado = Convert.ToInt32(com.Parameters["@Exito"].Value);
                Salida.mensaje = com.Parameters["Mensaje"].Value.ToString();
            }

            return Salida;
        }

        public clsSalida ActualisaMedicamento(clsMedicamento Medicamento)
        {
            clsSalida Salida = new clsSalida();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand com = new SqlCommand("sp_actualiza_medicamento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@IdMedicamento", Medicamento.IdMedicamento);
                com.Parameters.AddWithValue("@Nombre", Medicamento.Nombre);
                com.Parameters.AddWithValue("@Concentracion", Medicamento.Concentracion);
                com.Parameters.AddWithValue("@Precio", Medicamento.Precio);
                com.Parameters.AddWithValue("@Presentacion", Medicamento.Presentacion);
                com.Parameters.AddWithValue("@Habilitado", Medicamento.Habilitado);
                com.Parameters.AddWithValue("@Acciones", Medicamento.Acciones);
                com.Parameters.AddWithValue("@IdFormaFarmaucetica", Medicamento.IdFormaFarmaucetica);
                com.Parameters.Add("@Exito", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;

                con.Open();

                com.ExecuteNonQuery();

                Salida.registrado = Convert.ToInt32(com.Parameters["@Exito"].Value);
                Salida.mensaje = com.Parameters["Mensaje"].Value.ToString();
            }

            return Salida;
        }

        public clsSalida EliminarMedicamento(int IdMedicamento)
        {
            clsSalida Salida = new clsSalida();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand com = new SqlCommand("sp_elimina_medicamento", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@IdMedicamento", IdMedicamento);
                com.Parameters.Add("@Exito", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;

                con.Open();

                com.ExecuteNonQuery();

                Salida.registrado = Convert.ToInt32(com.Parameters["@Exito"].Value);
                Salida.mensaje = com.Parameters["Mensaje"].Value.ToString();
            }

            return Salida;
        }

        public List<clsMedicamento> ConsultaMedicamentos(int ignorar_primeros, int cantidad_filas, string filtro)
        {

            List<clsMedicamento> lista = new List<clsMedicamento>();

            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("sp_ConsultaMedicamentos", con);
                cmd.Parameters.AddWithValue("Ignorar", ignorar_primeros);
                cmd.Parameters.AddWithValue("Cantidad", cantidad_filas);
                cmd.Parameters.AddWithValue("filtro", filtro);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new clsMedicamento()
                        {
                            IdMedicamento = Convert.ToInt32(dr["IdMedicamento"]),
                            Nombre = dr["Nombre"].ToString(),
                            Concentracion = dr["Concentracion"].ToString(),
                            Precio   = dr["Precio"].ToString(),
                            Presentacion = dr["Presentacion"].ToString(),
                            Habilitado = dr["Habilitado"].ToString(),
                            Acciones = dr["Acciones"].ToString(),
                            IdFormaFarmaucetica = Convert.ToInt32(dr["IdFormaFarmaucetica"]),
                        });
                    }
                }

            }

            return lista;
        }
    }
}