using Agenda_Virtual.Clases.DB;
using Agenda_Virtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agenda_Virtual.Servicios
{
    public class VisitaServicecs
    {

        public List<Agenda_Virtual.Models.Visita> MostrarTodasVisita()
        {
            var db = new AgendaContext();
            var consulta = from u in db.Visitas
                           select u;
            var listaARetornar = new List<Visita>();

            foreach (var item in consulta)
            {
                listaARetornar.Add(new Visita()
                {
                    Alumno = item.Alumno,
                    Padre = item.Padre,
                    Telefono = item.Telefono,
                    Escuela = item.Escuela,
                    Grado = item.Grado,
                    Municipio = item.Municipio,
                    Observacion = item.Observacion
                });
            }
            return listaARetornar;
        }
               
    }
}
