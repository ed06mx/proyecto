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
    public class BaseDatosUsuario
    {
        //declare connection string
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        //Method for Adding an Employee
        public clsSalida AltaUsuario(clsUsuario Usuario)
        {
            clsSalida Salida = new clsSalida();
            using (SqlConnection con = new SqlConnection(cs))
            {
                
                SqlCommand com = new SqlCommand("sp_alta_usuario", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nombre", Usuario.Nombre);
                com.Parameters.AddWithValue("@Fecha_Creacion",DateTime.Now);
                com.Parameters.AddWithValue("@Usuario", Usuario.Usuario);
                com.Parameters.AddWithValue("@Password", Usuario.Password);
                com.Parameters.AddWithValue("@Estatus", Usuario.Estatus);
                com.Parameters.AddWithValue("@Acciones", Usuario.Acciones);
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

        public clsSalida ActualisaUsuario(clsUsuario Usuario)
        {
            clsSalida Salida = new clsSalida();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand com = new SqlCommand("sp_actualisa_usuario", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@idUsuario", Usuario.IdUsuario);
                com.Parameters.AddWithValue("@Nombre", Usuario.Nombre);                
                com.Parameters.AddWithValue("@Usuario", Usuario.Usuario);
                com.Parameters.AddWithValue("@Password", Usuario.Password);
                com.Parameters.AddWithValue("@Estatus", Usuario.Estatus);
                com.Parameters.AddWithValue("@Acciones", Usuario.Acciones);
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

        public clsSalida EliminarUsuario(int IdUsuario)
        {
            clsSalida Salida = new clsSalida();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand com = new SqlCommand("sp_elimina_usuario", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@idUsuario", IdUsuario);              
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

        public List<clsUsuario> ConsultaUsuario(int ignorar_primeros, int cantidad_filas, string filtro)
        {

            List<clsUsuario> lista = new List<clsUsuario>();

            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("sp_ConsultaUsuarios", con);
                cmd.Parameters.AddWithValue("Ignorar", ignorar_primeros);
                cmd.Parameters.AddWithValue("Cantidad", cantidad_filas);
                cmd.Parameters.AddWithValue("filtro", filtro);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new clsUsuario()
                        {
                            IdUsuario = Convert.ToInt32( dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Usuario = dr["Usuario"].ToString(),
                            Password = dr["Password"].ToString(),
                            Estatus = dr["Estatus"].ToString(),
                            Acciones = dr["Acciones"].ToString(),
                            Fecha_Creacion = dr["Fecha_Creacion"].ToString()
                        });
                    }
                }

            }

            return lista;
        }
    }
}