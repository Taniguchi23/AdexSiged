using SIGED_API.Entity;
using System;
using System.Collections.Generic;

namespace SIGED_API.Models
{
    public class SeleccionInformacion
    {
        public Revision RevisionCV { get; set; }

        public E_Competencia E_Competencia { get; set; }

        public E_Tecnica E_Tecnica { get; set; }

        public E_JefeAcademico E_JefeAcademico { get; set; }

        //public RequestCompetencia E_Competenciab { get; set; }
    }
}
