using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cepdi_Prueba.Models;

namespace Prueba_Cepdi.Controllers
{
    public class LoginController : Controller
    {
        static string cadena = "Server=localhost;Database=Cepdi_prueba;Trusted_Connection=True;";

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(clsAutenticacion cLogin)
        {
            //cLogin.Password = ConvertirSha256(cLogin.Password);

            using (SqlConnection cn = new SqlConnection(cadena))
            {

                SqlCommand cmd = new SqlCommand("sp_Valida_Usuario", cn);
                cmd.Parameters.AddWithValue("Usuario", cLogin.Usuario);
                cmd.Parameters.AddWithValue("Password", cLogin.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cLogin.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (cLogin.IdUsuario != 0)
            {

                Session["usuario"] = cLogin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}