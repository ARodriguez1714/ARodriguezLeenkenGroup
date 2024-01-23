using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAllEstado()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezLeenkenGroupContext context =  new DL.ArodriguezLeenkenGroupContext())
                {
                    var query = context.CantEntidadFederativas.FromSqlRaw($"EstadoGetAll");

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            estado.IdEstado = item.IdEstado;
                            estado.Nombre = item.Nombre;

                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
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
