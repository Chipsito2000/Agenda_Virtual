using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda_Virtual.Clases.DB;
using Agenda_Virtual.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Drawing;

namespace Agenda_Virtual.Controllers
{
    public class VisitaController : Controller
    {
        //vista del formulario
        public IActionResult Visita()
        {
            return View();
        }


        // POST: Crear nueva visita       
        [HttpPost]
        public ActionResult Crear([Bind("Id,Alumno,Padre,Telefono,Escuela,Grado,Municipio,Observacion")] Visita visita)
        {
            //Conexion bdd
            var db = new AgendaContext();

            //Metodo de validacion
            if (ModelState.IsValid)
            {
                //Agrega la conexion
                db.Add(visita);
                //Guarda el nuevo formulario
                db.SaveChanges();
                //Retorna a vista
                return RedirectToAction("Visita", "Visita");
            }
            return View();
        }

        public ActionResult ver()
        {
            //Asigna la bdd en lista
            IList<Visita> visitaList = new List<Visita>();
            //Guarda la lista en un almacen temporal
            ViewData["visita"] = visitaList;
            //Retorna a vista
            return View();
        }

        public async Task<IActionResult> Registro(string buscar)
        {
            //Conexion a la bdd
            var db = new AgendaContext();
            //Buscar
            var usuarios= from Alumno in db.Visitas select Alumno;

            if (!String.IsNullOrEmpty(buscar))
            {
                usuarios = db.Visitas.Where(s => s.Alumno!.Contains(buscar));
            }

            //Retorna datos a mostrar
            return View(await usuarios.ToListAsync());
                

        }




        // GET: Metodo identificador
        public async Task<IActionResult> Delete(int? id)
        {
            //Conexion a bdd
            var db = new AgendaContext();
            //Metodos para verificar que haya informacion 
            if (id == null || db.Visitas == null)
            {
                return NotFound();
            }

            var visita = db.Visitas.Find(id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Funcion borrar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Conexion a la base de datos
            var db = new AgendaContext();
            //Asignacion de elementos
            var visita = await db.Visitas.FindAsync(id);
            //Elimina datos de la lista
            db.Visitas.RemoveRange(db.Visitas.ToList());
            //Actualiza la bdd
            await db.SaveChangesAsync();
            //Redireccion a vista
            return RedirectToAction("Registro", "Visita");
        }

        //Exportar datos
        public IActionResult ExportarExcel()
        {
            var db = new AgendaContext();

            //Crea el libro excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Llama a los datos
            var visitas = db.Visitas.AsNoTracking().ToList();
            using (var libro = new ExcelPackage())
            {
                //Se especifican datos en la hoja excel
                var worksheet = libro.Workbook.Worksheets.Add("Visita");
                worksheet.Cells["A1"].Value = "Reporte";
                worksheet.Cells["A1"].Style.Font.Color.SetColor(Color.Red);
                worksheet.Cells["A1"].Style.Font.Size = 30;
                worksheet.Cells["B1"].Value = "1";
                worksheet.Cells["C1"].Value = "2";
                worksheet.Cells["D1"].Value = "3";
                worksheet.Cells["E1"].Value = "4";
                worksheet.Cells["F1"].Value = "5";
                worksheet.Cells["G1"].Value = "6";
                worksheet.Cells["H1"].Value = "7";

                worksheet.Cells["A2"].Value = "Dia";
                worksheet.Cells["A2"].Style.Font.Color.SetColor(Color.Red);
                worksheet.Cells["A2"].Style.Font.Size = 20;
                worksheet.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);


                worksheet.Cells["A4"].LoadFromCollection(visitas, PrintHeaders: true);
                for (var col = 1; col < visitas.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                // Crea formato de la tabla
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: visitas.Count + 1, toColumn: 8), "Visitas");
                tabla.ShowHeader = true;
                //tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = true;

                //Descarga archvio
                return File(libro.GetAsByteArray(), excelContentType, "Registro.xlsx");
            }
        }
    }

}