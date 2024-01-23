using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezLeenkenGroupContext context = new DL.ArodriguezLeenkenGroupContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();

                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.IdEmpleado = item.IdEmpleado;
                        empleado.NumeroNomina = item.NumeroNomina;
                        empleado.Nombre = item.Nombre;
                        empleado.ApellidoPaterno = item.ApellidoPaterno;
                        empleado.ApellidoMaterno = item.ApellidoMaterno;

                        //Estado - inicializamos la propiedad de navegación
                        empleado.Estado = new ML.Estado();
                        empleado.Estado.IdEstado = item.IdEstado.Value;
                        empleado.Estado.Nombre = item.NombreEstado;

                        result.Objects.Add(empleado);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "OCURRIO UN ERROR CON LA CONSULTA" + result.Ex;
                throw;
            }

            return result;
        }
        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezLeenkenGroupContext context = new DL.ArodriguezLeenkenGroupContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").ToList().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.IdEmpleado = query.IdEmpleado;
                        empleado.NumeroNomina = query.NumeroNomina; 
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;

                        //Estado - inicializamos la propiedad de navegación
                        empleado.Estado = new ML.Estado();
                        empleado.Estado.IdEstado = query.IdEstado.Value;
                        empleado.Estado.Nombre = query.NombreEstado;

                        result.Object = empleado;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "EL USUARIO NO EXISTE";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "OCURRIO UN ERROR CON LA CONSULTA" + result.Ex;
                throw;
            }

            return result;
        }
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezLeenkenGroupContext context = new DL.ArodriguezLeenkenGroupContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', {empleado.Estado.IdEstado}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "EL EMPLEADO SE AGREGO CORRECTAMENTE";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "OCURRIO UN ERROR AL AGREGAR AL EMPLEADO" + result.Ex;
                throw;
                throw;
            }

            return result;
        }
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezLeenkenGroupContext context = new DL.ArodriguezLeenkenGroupContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate {empleado.IdEmpleado}, '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', {empleado.Estado.IdEstado}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "EL EMPLEADO SE ACTUALIZO CORRECTAMENTE";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "OCURRIO UN ERROR CON LA CONSULTA" + result.Ex;
                throw;
            }

            return result;
        }
        public static ML.Result Delete(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezLeenkenGroupContext context = new DL.ArodriguezLeenkenGroupContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {IdEmpleado}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "USUARIO ELIMINADO CORRECTAMENTE";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "USUARIO NO FUE ELIMINADO CORRECTAMENTE";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "OCURRIO UN ERROR CON LA CONSULTA" + result.Ex;
                throw;
            }

            return result;
        }
    }
}