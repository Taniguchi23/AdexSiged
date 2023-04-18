using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGED_API.Contexts;
using SIGED_API.Entity;
using SIGED_API.Models;
using SIGED_API.Models.Dao;
using SIGED_API.Models.Response;
using SIGED_API.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static SIGED_API.Models.FichaDatosRequest;
using Postulante = SIGED_API.Ficha.Postulante;
using Postulantes = SIGED_API.Models.Postulante;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGED_API.Controllers
{
    [Route("api/ficha")]
    [ApiController]
    //[Authorize]
    public class FichaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly AppDbContext2 context2;
        private readonly AppDbContext3 context3;
        private readonly AppDbContext4 context4;
        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly ILogger<FichaController> logger;

        public FichaController(AppDbContext3 context3, AppDbContext context, AppDbContext2 context2, AppDbContext4 context4, IWebHostEnvironment webHost)
        {
            this.context3 = context3;
            this.context2 = context2;
            this.context4 = context4;
            this.context = context;
            webHostEnviroment = webHost;
        }
        // GET: api/<FichaController>
        [HttpGet]
        public IEnumerable<Postulante> GetFicha()
        {
            try
            {
                return context3.Postulante.ToList();
                //return Postulante();
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error" + ex.Message);
                throw;
            }
        }

        // GET api/<FichaController>/5
        [HttpGet("{id}")]
        public  Respuesta Get(int id)
        {
          var respuesta = new Respuesta();
            respuesta.status = false;
            var postulante =  context.Postulante.FirstOrDefault(p => p.postulante_id == id);
            
        
            if (postulante == null)
            {
                respuesta.Data = "No existe el postulante.";
                return respuesta;
            }
            
            var estudio_realizado =   context3.Estudio_Realizado.FirstOrDefault(e => e.postulante_id == id);
            if (estudio_realizado == null)
            {
                respuesta.Data = "No existe una ficha para ese postulante.";
                return respuesta;
            }

            var ficha = new FichaDao();

            ficha.Postulanteid = id;
            postulante.contrasena = "";
            ficha.Postulante = postulante;
           // var postulante = context3.Postulante.FirstOrDefault(p = postulante.postulante_id == id); 

            var pregrados =   context3.Pregrado.Where(p => p.estudio_id == estudio_realizado.estudio_id).ToList();
            var postgrados =  context3.Postgrado.Where(p => p.estudio_id == estudio_realizado.estudio_id).ToList();
            var nivelIngles =  context3.NIVEL_INGLES.Where(p => p.ESTUDIO_ID == estudio_realizado.estudio_id).ToList();
            var nivelOfimatica =  context3.NIVEL_OFIMATICA.Where(p => p.ESTUDIO_ID == estudio_realizado.estudio_id).ToList();

            ficha.Pregrados = pregrados;
            ficha.Postgrados = postgrados;
            ficha.NivelIngles= nivelIngles;
            ficha.NivelOfimatica = nivelOfimatica;

            var experiencia =  context3.EXPERIENCIA.FirstOrDefault(e => e.POSTULANTE_ID == id);
              if (experiencia != null)
               {
                   var experienciaLaboral = context3.EXPERIENCIA_LABORAL.Where(p => p.EXPERIENCIA_ID == experiencia.EXPERIENCIA_ID).ToList();
                   ficha.ExperienciaLaboral= experienciaLaboral;
               }

           

            var composicionFamiliar =  context2.COMPOSICION_FAMILIAR.FirstOrDefault(c => c.POSTULANTE_ID == id);
            if (composicionFamiliar != null)
            {
                var composicionHijos = context3.COMPOSICION_HIJO.Where(c => c.COMPOSICION_ID == composicionFamiliar.COMPOSICION_ID).ToList();
                ficha.ComposicionHijo= composicionHijos;
            }
            var pago =  context3.PAGO.FirstOrDefault(p => p.POSTULANTE_ID == id);
            var declaracionJurada =  context3.DECLARACION_JURADA.FirstOrDefault(d => d.postulante_id == id);

            ficha.CommposicionFamiliar = composicionFamiliar;
            ficha.PAGO = pago;
            ficha.DeclaracionJurada = declaracionJurada;
            
            respuesta.Data = ficha;
            respuesta.status = true;
       
            return respuesta;
        }

        //POST api/<FichaController>
        [HttpPost]
        public Respuesta Post([FromBody] FichaDatosRequest ficha)
        {
            var postulante = context.Postulante.FirstOrDefault(p=>p.postulante_id == ficha.postulante_id); 
            if (postulante != null)
            {
                Postulantes postulantes = new Postulantes();
                postulantes.postulante_id = ficha.postulante_id;
                postulantes.nombre = ficha.nombre != null ?  ficha.nombre : postulante.nombre;
                postulantes.ape_paterno = ficha.ape_paterno != null? ficha.ape_paterno : postulante.ape_paterno;
                postulantes.ape_materno = ficha.ape_materno != null? ficha.ape_materno : postulante.ape_materno;
                postulantes.numero = ficha.dni != null ? ficha.dni : postulantes.numero;
                postulantes.fec_nacimiento = ficha.fec_nacimiento != null ? ficha.fec_nacimiento : postulante.fec_nacimiento;
                postulantes.celular = ficha.celular != null ? ficha.celular : postulante.celular;
                postulantes.correo = ficha.correo !=null ? ficha.correo : postulante.correo;
                postulantes.contrasena = ficha.contrasena != null ? Encrypt.GetSHA256(ficha.contrasena) : postulante.contrasena;
                postulantes.imageurl = ficha.imageurl != null ? ficha.imageurl : postulante.imageurl;
                postulantes.archivocv = ficha.archivocv != null? ficha.archivocv :  postulante.archivocv;
                postulantes.genero = ficha.genero != null ? ficha.genero : postulante.genero;
                postulantes.departamento_id = ficha.departamento_id != null ? ficha.departamento_id :postulante.departamento_id;
                postulantes.provincia_id = ficha.provincia_id != null? ficha.provincia_id : postulante.provincia_id;
                postulantes.distrito_id = ficha.distrito_id != null? ficha.distrito_id : postulante.distrito_id;
                postulantes.estado_id = ficha.estado_id != null? ficha.estado_id : postulante.estado_id;
                postulantes.correo_adex = ficha.correo_adex!=null? ficha.correo_adex :  postulante.correo_adex;
                postulantes.telefono_fijo = ficha.telefono_fijo != null ? ficha.telefono_fijo : postulante.telefono_fijo;
                postulantes.telefono_emergencia = ficha.telefono_emergencia!= null ? ficha.telefono_emergencia: postulante.telefono_emergencia;
                postulantes.via_id = ficha.via_id!=null? ficha.via_id : postulante.via_id;
                postulantes.nombre_via =  ficha.nombre_via!=null? ficha.nombre_via:postulante.nombre_via;
                postulantes.NroMzLote = ficha.NroMzLote !=null? ficha.NroMzLote:postulante.NroMzLote;
                postulantes.interior = ficha.interior!=null? ficha.interior: postulante.interior;
                postulantes.departamento_id_dir = ficha.departamento_id_dir != null ? ficha.departamento_id_dir : postulante.departamento_id_dir;
                postulantes.provincia_id_dir = ficha.provincia_id_dir != null? ficha.provincia_id_dir :postulante.provincia_id_dir;
                postulantes.distrito_id_dir = ficha.distrito_id_dir != null ? ficha.distrito_id_dir : postulante.distrito_id_dir;
                postulantes.tiene_familiar = ficha.tiene_familiar != null ? ficha.tiene_familiar : postulante.tiene_familiar;
                postulantes.nombre_familiar = ficha.nombre_familiar != null ? ficha.nombre_familiar : postulante.nombre_familiar;
                postulantes.area_id = ficha.area_id!=null? ficha.area_id : postulante.area_id;
                postulantes.referido_linkedin =  ficha.referido_linkedin!=null?(ficha.referido_linkedin==true?1:0):(postulante.referido_linkedin);
                postulantes.otros_medio = ficha.otros_medio!=null? ficha.otros_medio :postulante.otros_medio;
                postulantes.persona_discapacidad = ficha.persona_discapacidad != null ? ficha.persona_discapacidad : postulante.persona_discapacidad;
                postulantes.tipo_discapacidad_id = ficha.tipo_discapacidad_id != null ? ficha.tipo_discapacidad_id : postulante.tipo_discapacidad_id;
                postulantes.certificado = ficha.certificado != null ? ficha.certificado : postulante.certificado;
                postulantes.num_certificado = ficha.num_certificado != null ? ficha.num_certificado : postulante.num_certificado;
                postulantes.rol_id = postulante.rol_id;
                postulantes.estado = postulante.estado;
               
                    context2.Entry(postulantes).State = EntityState.Modified;
                    context2.SaveChanges();
                
            }
            var respuesta = new Respuesta();
            respuesta.status = false;
            var postulanteFicha = context3.Estudio_Realizado.FirstOrDefault(e => e.postulante_id==ficha.postulante_id);
            

            TEMPORAL_IMAGEN temporal_imagen = context3.TEMPORAL_IMAGEN.FirstOrDefault(p => p.TIPOARCHIVO == 1 & p.MODULO == 2);
            if (postulanteFicha == null)
            {
                try
                {
                   
                    Estudio_Realizado oestudio = new Estudio_Realizado();
                    oestudio.postulante_id = ficha.postulante_id;
                    oestudio.otros = ficha.otros;
                    oestudio.otros_programas = ficha.otros_programas;
                    context3.Estudio_Realizado.Add(oestudio);
                    context3.SaveChanges();
                    foreach (var opregrados in ficha.Pregrados)
                    {
                        Pregrado opregrado = new Pregrado();
                        opregrado.estudio_id = oestudio.estudio_id;
                        opregrado.centro_estudio = opregrados.centro_estudio;
                        opregrado.carrera = opregrados.carrera;
                        opregrado.grado_acad = opregrados.grado_acad;
                        opregrado.fecha_ingreso = opregrados.fecha_ingreso;
                        opregrado.fecha_salida = opregrados.fecha_salida;
                        context3.Pregrado.Add(opregrado);
                        context3.SaveChanges();
                    }


                    foreach (var opostgrados in ficha.Postgrados)
                    {
                        Postgrado opostgrado = new Postgrado();
                        opostgrado.estudio_id = oestudio.estudio_id;
                        opostgrado.centro_estudio = opostgrados.centro_estudio;
                        opostgrado.especializacion = opostgrados.especializacion;
                        opostgrado.nivel = opostgrados.nivel;
                        opostgrado.fecha_ingreso = opostgrados.fecha_ingreso;
                        opostgrado.fecha_salida = opostgrados.fecha_salida;
                        context3.Postgrado.Add(opostgrado);
                        context3.SaveChanges();
                    }




                    foreach (var oPostingles in ficha.Idioma_Ingles)
                    {
                        NIVEL_INGLES oingles = new NIVEL_INGLES();
                        oingles.ESTUDIO_ID = oestudio.estudio_id;
                        oingles.IDIOMA_ID = oPostingles.IDIOMA_ID;
                        oingles.NIVELESTUDIO_ID = oPostingles.NIVELESTUDIO_ID;
                        context3.NIVEL_INGLES.Add(oingles);
                        context3.SaveChanges();
                    }





                    foreach (var oPostofimatica in ficha.Ofimatica)
                    {
                        NIVEL_OFIMATICA oofimatica = new NIVEL_OFIMATICA();
                        oofimatica.ESTUDIO_ID = oestudio.estudio_id;
                        oofimatica.OFIMATICA_ID = oPostofimatica.OFIMATICA_ID;
                        oofimatica.NIVELESTUDIO_ID = oPostofimatica.NIVELESTUDIO_ID;
                        context3.NIVEL_OFIMATICA.Add(oofimatica);
                        context3.SaveChanges();
                    }


                    EXPERIENCIA oexperiencia = new EXPERIENCIA();
                    oexperiencia.POSTULANTE_ID = ficha.postulante_id;
                    context3.EXPERIENCIA.Add(oexperiencia);
                    context3.SaveChanges();



                    foreach (var oPostexperiencialaboral in ficha.Experiencia)
                    {
                        EXPERIENCIA_LABORAL oexperiencialaboral = new EXPERIENCIA_LABORAL();
                        oexperiencialaboral.EXPERIENCIA_ID = oexperiencia.EXPERIENCIA_ID;
                        oexperiencialaboral.EMPRESA = oPostexperiencialaboral.EMPRESA;
                        oexperiencialaboral.CARGO = oPostexperiencialaboral.CARGO;
                        oexperiencialaboral.JEFE_INMEDIATO = oPostexperiencialaboral.JEFE_INMEDIATO;
                        oexperiencialaboral.TELEFONO = oPostexperiencialaboral.TELEFONO;
                        oexperiencialaboral.FECHA_INGRESO = oPostexperiencialaboral.FECHA_INGRESO;
                        oexperiencialaboral.FECHA_CESE = oPostexperiencialaboral.FECHA_CESE;
                        oexperiencialaboral.MOTIVO_CESE = oPostexperiencialaboral.MOTIVO_CESE;
                        context3.EXPERIENCIA_LABORAL.Add(oexperiencialaboral);
                        context3.SaveChanges();
                    }


                    COMPOSICION_FAMILIAR ocomposicionfamiliar = new COMPOSICION_FAMILIAR();
                    ocomposicionfamiliar.POSTULANTE_ID = ficha.postulante_id;
                    ocomposicionfamiliar.NOMBRE = ficha.nombre;
                    ocomposicionfamiliar.apellido_paterno = ficha.apellido_paterno;
                    ocomposicionfamiliar.APELLIDO_MATERNO = ficha.apellido_materno;
                    ocomposicionfamiliar.DNI = ficha.dni;
                    ocomposicionfamiliar.FECHA = ficha.fecha;
                    ocomposicionfamiliar.EDAD = ficha.edad;
                    context3.COMPOSICION_FAMILIAR.Add(ocomposicionfamiliar);
                    context3.SaveChanges();


                    foreach (var oPostcomposicionhijo in ficha.Hijos)
                    {
                        COMPOSICION_HIJO ocomposicionhijo = new COMPOSICION_HIJO();
                        ocomposicionhijo.COMPOSICION_ID = ocomposicionfamiliar.COMPOSICION_ID;
                        ocomposicionhijo.NOMBRE = oPostcomposicionhijo.NOMBRE;
                        ocomposicionhijo.APELLIDO_PATERNO = oPostcomposicionhijo.APELLIDO_PATERNO;
                        ocomposicionhijo.APELLIDO_MATERNO = oPostcomposicionhijo.APELLIDO_MATERNO;
                        ocomposicionhijo.DNI = oPostcomposicionhijo.DNI;
                        ocomposicionhijo.FECHA = oPostcomposicionhijo.FECHA;
                        ocomposicionhijo.EDAD = oPostcomposicionhijo.EDAD;
                        context3.COMPOSICION_HIJO.Add(ocomposicionhijo);
                        context3.SaveChanges();
                    }

                    PAGO opago = new PAGO();
                    opago.POSTULANTE_ID = ficha.postulante_id;
                    opago.NRO_CUENTA = ficha.nro_cuenta;
                    opago.BANCO_ID = ficha.banco_id;
                    opago.CCI = ficha.cci;
                    opago.SISTEMA_PEN = ficha.sistema_pen;
                    opago.AFP_ID = ficha.afp_id;
                    opago.OTROS_BANCOS = ficha.otrosbancos;
                    context3.PAGO.Add(opago);
                    context3.SaveChanges();



                    if (temporal_imagen != null)
                    {

                        //ommodelo.referencia = temporal_imagen.archivo;
                        context3.TEMPORAL_IMAGEN.Remove(temporal_imagen);
                        context3.SaveChanges();

                    }

                    DECLARACION_JURADA odeclaracion = new DECLARACION_JURADA();
                    odeclaracion.postulante_id = ficha.postulante_id;
                    odeclaracion.fecha = ficha.fecha;
                    odeclaracion.firma = temporal_imagen.ARCHIVO;
                    context3.DECLARACION_JURADA.Add(odeclaracion);
                    context3.SaveChanges();

                    var objetoJson = new
                    {
                        IdFicha = oestudio.estudio_id
                    };

                    respuesta.status = true;
                    respuesta.Data = objetoJson;



                }
                catch (Exception ex)
                {
                    return respuesta;

                }
            }
            else
            {
                try
                {
                  //  var temporal_imagen = context3.TEMPORAL_IMAGEN.FirstOrDefault(p => p.TIPOARCHIVO == 1 & p.MODULO == 2);
                    //   Estudio_Realizado oestudio = new Estudio_Realizado();
                  //  if(ficha.postulante_id != null) postulanteFicha.postulante_id = ficha.postulante_id;
                    if(ficha.otros != null) postulanteFicha.otros = ficha.otros;
                    if(ficha.otros_programas != null) postulanteFicha.otros_programas = ficha.otros_programas;
                  //  context3.Estudio_Realizado.Add(postulanteFicha);
                    context3.SaveChanges();

                    if(ficha.Pregrados != null)
                    {
                        var pregrados = context3.Pregrado.Where(p=> p.estudio_id == postulanteFicha.estudio_id).ToList();
                        context3.Pregrado.RemoveRange(pregrados);
                        context3.SaveChanges() ;
                        
                        foreach(var opregrados in ficha.Pregrados)
                        {
                            Pregrado opregrado = new Pregrado();
                            opregrado.estudio_id = postulanteFicha.estudio_id;
                            opregrado.centro_estudio = opregrados.centro_estudio;
                            opregrado.carrera = opregrados.carrera;
                            opregrado.grado_acad = opregrados.grado_acad;
                            opregrado.fecha_ingreso = opregrados.fecha_ingreso;
                            opregrado.fecha_salida = opregrados.fecha_salida;
                            context3.Pregrado.Add(opregrado);
                            context3.SaveChanges();
                        }
                    }

                    if(ficha.Postgrados != null) {
                        var postgrados = context3.Postgrado.Where(p=> p.estudio_id == postulanteFicha.estudio_id).ToList();
                        context3.Postgrado.RemoveRange(postgrados);
                        context3.SaveChanges();

                        foreach (var opostgrados in ficha.Postgrados)
                        {
                            Postgrado opostgrado = new Postgrado();
                            opostgrado.estudio_id = postulanteFicha.estudio_id;
                            opostgrado.centro_estudio = opostgrados.centro_estudio;
                            opostgrado.especializacion = opostgrados.especializacion;
                            opostgrado.nivel = opostgrados.nivel;
                            opostgrado.fecha_ingreso = opostgrados.fecha_ingreso;
                            opostgrado.fecha_salida = opostgrados.fecha_salida;
                            context3.Postgrado.Add(opostgrado);
                            context3.SaveChanges();
                        }
                    }
                    
                    if(ficha.Idioma_Ingles != null)
                    {
                        var idiomas_ingles = context3.NIVEL_INGLES.Where(p => p.ESTUDIO_ID == postulanteFicha.estudio_id).ToList();
                        context3.NIVEL_INGLES.RemoveRange(idiomas_ingles);
                        context3.SaveChanges();
                        foreach (var oPostingles in ficha.Idioma_Ingles)
                        {
                            NIVEL_INGLES oingles = new NIVEL_INGLES();
                            oingles.ESTUDIO_ID = postulanteFicha.estudio_id;
                            oingles.IDIOMA_ID = oPostingles.IDIOMA_ID;
                            oingles.NIVELESTUDIO_ID = oPostingles.NIVELESTUDIO_ID;
                            context3.NIVEL_INGLES.Add(oingles);
                            context3.SaveChanges();
                        }
                    }

                    if(ficha.Ofimatica != null)
                    {
                        var ofimatica = context3.NIVEL_OFIMATICA.Where(p => p.ESTUDIO_ID == postulanteFicha.estudio_id).ToList();
                        context3.NIVEL_OFIMATICA.RemoveRange(ofimatica);
                        context3.SaveChanges();

                        foreach (var oPostofimatica in ficha.Ofimatica)
                        {
                            NIVEL_OFIMATICA oofimatica = new NIVEL_OFIMATICA();
                            oofimatica.ESTUDIO_ID = postulanteFicha.estudio_id;
                            oofimatica.OFIMATICA_ID = oPostofimatica.OFIMATICA_ID;
                            oofimatica.NIVELESTUDIO_ID = oPostofimatica.NIVELESTUDIO_ID;
                            context3.NIVEL_OFIMATICA.Add(oofimatica);
                            context3.SaveChanges();
                        }
                    }

                   
                    if(ficha.Experiencia != null)
                    {
                        var experiencia = context3.EXPERIENCIA.FirstOrDefault(e => e.POSTULANTE_ID == ficha.postulante_id);

                        if (experiencia != null)
                        {
                           var experiencias = context3.EXPERIENCIA_LABORAL.Where(e=> e.EXPERIENCIA_ID == experiencia.EXPERIENCIA_ID).ToList();
                            context3.EXPERIENCIA_LABORAL.RemoveRange(experiencias);
                            context3.SaveChanges() ;
                            foreach (var oPostexperiencialaboral in ficha.Experiencia)
                            {
                                EXPERIENCIA_LABORAL oexperiencialaboral = new EXPERIENCIA_LABORAL();
                                oexperiencialaboral.EXPERIENCIA_ID = experiencia.EXPERIENCIA_ID;
                                oexperiencialaboral.EMPRESA = oPostexperiencialaboral.EMPRESA;
                                oexperiencialaboral.CARGO = oPostexperiencialaboral.CARGO;
                                oexperiencialaboral.JEFE_INMEDIATO = oPostexperiencialaboral.JEFE_INMEDIATO;
                                oexperiencialaboral.TELEFONO = oPostexperiencialaboral.TELEFONO;
                                oexperiencialaboral.FECHA_INGRESO = oPostexperiencialaboral.FECHA_INGRESO;
                                oexperiencialaboral.FECHA_CESE = oPostexperiencialaboral.FECHA_CESE;
                                oexperiencialaboral.MOTIVO_CESE = oPostexperiencialaboral.MOTIVO_CESE;
                                context3.EXPERIENCIA_LABORAL.Add(oexperiencialaboral);
                                context3.SaveChanges();
                            }

                        }
                        else
                        {
                            EXPERIENCIA oexperiencia = new EXPERIENCIA();
                            oexperiencia.POSTULANTE_ID = ficha.postulante_id;
                            context3.EXPERIENCIA.Add(oexperiencia);
                            context3.SaveChanges();
                            foreach (var oPostexperiencialaboral in ficha.Experiencia)
                            {
                                EXPERIENCIA_LABORAL oexperiencialaboral = new EXPERIENCIA_LABORAL();
                                oexperiencialaboral.EXPERIENCIA_ID = experiencia.EXPERIENCIA_ID;
                                oexperiencialaboral.EMPRESA = oPostexperiencialaboral.EMPRESA;
                                oexperiencialaboral.CARGO = oPostexperiencialaboral.CARGO;
                                oexperiencialaboral.JEFE_INMEDIATO = oPostexperiencialaboral.JEFE_INMEDIATO;
                                oexperiencialaboral.TELEFONO = oPostexperiencialaboral.TELEFONO;
                                oexperiencialaboral.FECHA_INGRESO = oPostexperiencialaboral.FECHA_INGRESO;
                                oexperiencialaboral.FECHA_CESE = oPostexperiencialaboral.FECHA_CESE;
                                oexperiencialaboral.MOTIVO_CESE = oPostexperiencialaboral.MOTIVO_CESE;
                                context3.EXPERIENCIA_LABORAL.Add(oexperiencialaboral);
                                context3.SaveChanges();
                            }
                        }
                        



                    }


                    if (ficha.Hijos !=null)
                    {
                        var composicion = context3.COMPOSICION_FAMILIAR.FirstOrDefault(c => c.POSTULANTE_ID == ficha.postulante_id);
                        if (composicion != null)
                        {
                            var hijos = context3.COMPOSICION_HIJO.Where(h=> h.COMPOSICION_ID == composicion.COMPOSICION_ID).ToList();
                            context3.RemoveRange(hijos);
                            context3.SaveChanges();

                            foreach(var oPostcomposicionhijo in ficha.Hijos)
                    {
                                COMPOSICION_HIJO ocomposicionhijo = new COMPOSICION_HIJO();
                                ocomposicionhijo.COMPOSICION_ID = composicion.COMPOSICION_ID;
                                ocomposicionhijo.NOMBRE = oPostcomposicionhijo.NOMBRE;
                                ocomposicionhijo.APELLIDO_PATERNO = oPostcomposicionhijo.APELLIDO_PATERNO;
                                ocomposicionhijo.APELLIDO_MATERNO = oPostcomposicionhijo.APELLIDO_MATERNO;
                                ocomposicionhijo.DNI = oPostcomposicionhijo.DNI;
                                ocomposicionhijo.FECHA = oPostcomposicionhijo.FECHA;
                                ocomposicionhijo.EDAD = oPostcomposicionhijo.EDAD;
                                context3.COMPOSICION_HIJO.Add(ocomposicionhijo);
                                context3.SaveChanges();
                            }
                        }
                        else
                        {
                            COMPOSICION_FAMILIAR ocomposicionfamiliar = new COMPOSICION_FAMILIAR();
                            ocomposicionfamiliar.POSTULANTE_ID = ficha.postulante_id;
                            ocomposicionfamiliar.NOMBRE = ficha.nombre;
                            ocomposicionfamiliar.apellido_paterno = ficha.apellido_paterno;
                            ocomposicionfamiliar.APELLIDO_MATERNO = ficha.apellido_materno;
                            ocomposicionfamiliar.DNI = ficha.dni;
                            ocomposicionfamiliar.FECHA = ficha.fecha;
                            ocomposicionfamiliar.EDAD = ficha.edad;
                            context3.COMPOSICION_FAMILIAR.Add(ocomposicionfamiliar);
                            context3.SaveChanges();


                            foreach (var oPostcomposicionhijo in ficha.Hijos)
                            {
                                COMPOSICION_HIJO ocomposicionhijo = new COMPOSICION_HIJO();
                                ocomposicionhijo.COMPOSICION_ID = ocomposicionfamiliar.COMPOSICION_ID;
                                ocomposicionhijo.NOMBRE = oPostcomposicionhijo.NOMBRE;
                                ocomposicionhijo.APELLIDO_PATERNO = oPostcomposicionhijo.APELLIDO_PATERNO;
                                ocomposicionhijo.APELLIDO_MATERNO = oPostcomposicionhijo.APELLIDO_MATERNO;
                                ocomposicionhijo.DNI = oPostcomposicionhijo.DNI;
                                ocomposicionhijo.FECHA = oPostcomposicionhijo.FECHA;
                                ocomposicionhijo.EDAD = oPostcomposicionhijo.EDAD;
                             //   context3.COMPOSICION_HIJO.Add(ocomposicionhijo);
                                context3.SaveChanges();
                            }
                        }
                        
                    }


                   var pago = context3.PAGO.FirstOrDefault(p => p.POSTULANTE_ID == ficha.postulante_id);
                    if (pago != null)
                    {
                        //pago.POSTULANTE_ID = ficha.postulante_id;
                        if(ficha.nro_cuenta != null) pago.NRO_CUENTA = ficha.nro_cuenta;
                        if(ficha.banco_id != null) pago.BANCO_ID = ficha.banco_id;
                        if (ficha.cci != null) pago.CCI = ficha.cci;
                        if (ficha.sistema_pen != null) pago.SISTEMA_PEN = ficha.sistema_pen;
                        if(ficha.afp_id != null) pago.AFP_ID = ficha.afp_id;
                        if (ficha.otrosbancos != null) pago.OTROS_BANCOS = ficha.otrosbancos;
                        //  context3.PAGO.Add(pago);

                       
                            //context2.Entry(postulantes).State = EntityState.Modified;
                            context3.SaveChanges();
                            //context2.SaveChanges();
                        
                      
                        
                    }
                    else
                    {
                        PAGO opago = new PAGO();
                        opago.POSTULANTE_ID = ficha.postulante_id;
                        opago.NRO_CUENTA = ficha.nro_cuenta;
                        opago.BANCO_ID = ficha.banco_id;
                        opago.CCI = ficha.cci;
                        opago.SISTEMA_PEN = ficha.sistema_pen;
                        opago.AFP_ID = ficha.afp_id;
                        opago.OTROS_BANCOS = ficha.otrosbancos;
                        context3.PAGO.Add(opago);
                        context3.SaveChanges();
                    }


                   

                

                    

                    var declaracion =  context3.DECLARACION_JURADA.FirstOrDefault(d => d.postulante_id == ficha.postulante_id);
                    if (declaracion != null)
                    {
                      //  DECLARACION_JURADA odeclaracion = new DECLARACION_JURADA();
                      //   declaracion.postulante_id = ficha.postulante_id;
                         if(ficha.fecha != null) declaracion.fecha = ficha.fecha;
                        if(temporal_imagen != null && temporal_imagen.ARCHIVO != null) declaracion.firma = temporal_imagen.ARCHIVO;
                      //  context3.DECLARACION_JURADA.Add(declaracion);
                        context3.SaveChanges();
                    }
                    else
                    {
                        DECLARACION_JURADA odeclaracion = new DECLARACION_JURADA();
                        odeclaracion.postulante_id = ficha.postulante_id;
                        odeclaracion.fecha = ficha.fecha;
                        odeclaracion.firma = temporal_imagen.ARCHIVO;
                        context3.DECLARACION_JURADA.Add(odeclaracion);
                        context3.SaveChanges();
                    }

                    if (temporal_imagen != null)
                    {

                        //ommodelo.referencia = temporal_imagen.archivo;
                        context3.TEMPORAL_IMAGEN.Remove(temporal_imagen);
                        context3.SaveChanges();

                    }

                    var objetoJson = new
                    {
                        IdFicha = postulanteFicha.estudio_id
                    };
                    respuesta.status = true;
                    respuesta.Data = objetoJson;

                //    return respuesta;


                }
                catch (Exception ex)
                {
                    return respuesta;

                }


            }
            return respuesta;
        }

        [HttpGet("TraerImagen/{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {

            var postulante = context3.DECLARACION_JURADA.FirstOrDefault(p => p.postulante_id == id);

            string path = webHostEnviroment.ContentRootPath + "\\images\\ficha\\";
            var filePath = path + postulante.firma;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            return null;
        }

        private string UploadedImagePostulante(TemporalRequest temporal)
        {
            string uniqueFileName = null;
            if (temporal.Imagen != null)
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.ContentRootPath, "images\\ficha");
                uniqueFileName = "ficha_"+Guid.NewGuid().ToString("N");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    temporal.Imagen.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }

       [HttpPost("AdjuntarImagen/{id}")]
        public ActionResult PostImagen([FromForm] TemporalRequest temporal, int id)
        {
            try
            {
                TEMPORAL_IMAGEN opostulante = new TEMPORAL_IMAGEN();
                string uniqueFileName = UploadedImagePostulante(temporal);
                opostulante.ARCHIVO = uniqueFileName;
                opostulante.DESCRIPCION = uniqueFileName;
                opostulante.TIPOARCHIVO = 1;
                opostulante.MODULO = 3;
                context3.TEMPORAL_IMAGEN.Add(opostulante);
                context3.SaveChanges();

                var postulante = context3.DECLARACION_JURADA.FirstOrDefault(p => p.postulante_id == id);
                if (postulante != null) 
                {
                    postulante.postulante_id = id;
                    postulante.firma = uniqueFileName;
                    context3.Entry(postulante).State = EntityState.Modified;
                    context3.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<FichaController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<FichaController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
