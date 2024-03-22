using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cepdi_Prueba.Validaciones;
using Cepdi_Prueba.Models;

namespace Cepdi_Prueba.Controllers
{
    [Sesion]
    public class HomeController : Controller
    {
        public object Usuario { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Medicamentos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Cerrar()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Login");
        }

        // public static DataTableResponse<clsUsuario> List(string ClientParameters)
        [HttpPost]
        public ActionResult List()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();           
            var sortColumDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var sortColum = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortName = Request.Form.GetValues("order[0][name]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            // var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[1][name]").FirstOrDefault();

            int PageSise;
            int Skip;

            List<clsUsuario> lsUsuario = new List<clsUsuario>();
            /*clsUsuario uno = new clsUsuario();
            uno.IdUsuario = 1;
            uno.Estatus = "1";
            uno.Acciones = "Algunas";
            uno.Nombre = "edgar";
            uno.Usuario = "uno";
            uno.Password = "test";
            uno.Fecha_Creacion = DateTime.Now.ToString();

            lsUsuario.Add(uno);
            uno = new clsUsuario();
            uno.IdUsuario = 44;
            uno.Estatus = "1";
            uno.Acciones = "Algunas";
            uno.Nombre = "Juan";
            uno.Usuario = "dos";
            uno.Password = "test";
            uno.Fecha_Creacion = DateTime.Now.ToString();
            lsUsuario.Add(uno);*/

            PageSise = length != null ? Convert.ToInt32(length) : 0;
            Skip = start!= null ? Convert.ToInt32(start) : 0;

            BaseDatosUsuario Base = new BaseDatosUsuario();

            lsUsuario = Base.ConsultaUsuario(Skip, 10, "");


            return Json(new { draw = draw, recordsFiltered = 1, recordsTotal = lsUsuario.Count, data = lsUsuario });
        }

        [HttpPost]
        public ActionResult AltaUsuario(string Nombre, string Usuario, string Password, string Estatus, string Acciones)
        {
            clsUsuario eUsuario = new clsUsuario();

            eUsuario.Nombre = Nombre;
            eUsuario.Fecha_Creacion = DateTime.Now.ToString();
            eUsuario.Usuario = Usuario;
            eUsuario.Password = Password;
            eUsuario.Estatus = Estatus;
            eUsuario.Acciones = Acciones;

            BaseDatosUsuario Base = new BaseDatosUsuario();
            clsSalida Salida = new clsSalida();

            Salida = Base.AltaUsuario(eUsuario);

            ViewData["Mensaje"] = Salida.mensaje;

            return Json(Salida.registrado, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ActualizacionUsuario(int IdUsuario,string Nombre, string Usuario, string Password, string Estatus, string Acciones)
        {
            clsUsuario eUsuario = new clsUsuario();

            eUsuario.IdUsuario = IdUsuario;
            eUsuario.Nombre = Nombre;            
            eUsuario.Usuario = Usuario;
            eUsuario.Password = Password;
            eUsuario.Estatus = Estatus;
            eUsuario.Acciones = Acciones;

            BaseDatosUsuario Base = new BaseDatosUsuario();
            clsSalida Salida = new clsSalida();

            Salida = Base.ActualisaUsuario(eUsuario);

            ViewData["Mensaje"] = Salida.mensaje;

                      
            return Json(Salida.registrado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EliminarUsuario(int IdUsuario)
        {          
            BaseDatosUsuario Base = new BaseDatosUsuario();

            clsSalida Salida = new clsSalida();

            Salida = Base.EliminarUsuario(IdUsuario);

            ViewData["Mensaje"] = Salida.mensaje;
                        
            return Json(Salida.registrado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ListMedicamentos()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var sortColum = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortName = Request.Form.GetValues("order[0][name]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            // var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[1][name]").FirstOrDefault();

            int PageSise;
            int Skip;

            List<clsMedicamento> lsMedicamento = new List<clsMedicamento>();
          
            PageSise = length != null ? Convert.ToInt32(length) : 0;
            Skip = start != null ? Convert.ToInt32(start) : 0;

            BaseDatosMedicamento Base = new BaseDatosMedicamento();

            lsMedicamento = Base.ConsultaMedicamentos(Skip, 10, "");


            return Json(new { draw = draw, recordsFiltered = 1, recordsTotal = lsMedicamento.Count, data = lsMedicamento });
        }

        [HttpPost]
        public ActionResult AltaMedicamento(string Nombre, string Concentracion, string Precio, string Presentacion, string Habilitado,string Acciones, int IdFormaFarmaucetica)
        {
            clsMedicamento Medicamento = new clsMedicamento();

            Medicamento.Nombre = Nombre;
            Medicamento.Concentracion = Concentracion;
            Medicamento.Precio = Precio;
            Medicamento.Presentacion = Presentacion;
            Medicamento.Habilitado = Habilitado;
            Medicamento.Acciones = Acciones;
            Medicamento.IdFormaFarmaucetica = IdFormaFarmaucetica;

            BaseDatosMedicamento Base = new BaseDatosMedicamento();
            clsSalida Salida = new clsSalida();

            Salida = Base.AltaMedicamento(Medicamento);

            ViewData["Mensaje"] = Salida.mensaje;

            return Json(Salida.registrado, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ActualizacionMedicamento(int IdMedicamento, string Nombre, string Concentracion, string Precio, string Presentacion, string Habilitado, string Acciones, int IdFormaFarmaucetica)
        {
            clsMedicamento Medicamento = new clsMedicamento();

            Medicamento.IdMedicamento = IdMedicamento;
            Medicamento.Nombre = Nombre;
            Medicamento.Concentracion = Concentracion;
            Medicamento.Precio = Precio;
            Medicamento.Presentacion = Presentacion;
            Medicamento.Habilitado = Habilitado;
            Medicamento.Acciones = Acciones;
            Medicamento.IdFormaFarmaucetica = IdFormaFarmaucetica;

            BaseDatosMedicamento Base = new BaseDatosMedicamento();
            clsSalida Salida = new clsSalida();

            Salida = Base.ActualisaMedicamento(Medicamento);

            ViewData["Mensaje"] = Salida.mensaje;

            return Json(Salida.registrado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EliminarMedicamento(int IdMedicamento)
        {
            BaseDatosMedicamento Base = new BaseDatosMedicamento();

            clsSalida Salida = new clsSalida();

            Salida = Base.EliminarMedicamento(IdMedicamento);

            ViewData["Mensaje"] = Salida.mensaje;

           return Json(Salida.registrado, JsonRequestBehavior.AllowGet);
        }
    }
}