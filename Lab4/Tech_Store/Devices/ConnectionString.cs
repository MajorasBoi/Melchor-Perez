using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ConnectionString
    {
        /// <summary>
        /// Inicializa un objeto <see cref="ConnectionString"/>.
        /// </summary>
        /// <param name="value">
        /// Cadena de conexión a la base de datos.
        /// </param>
        public ConnectionString(string value) => Value = value;

        /// <summary>
        /// Cadena de conexión.
        /// </summary>
        public string Value { get; }
    }
}
