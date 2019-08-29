using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    public class DBObjConverter
    {
        public T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
        }
    }
}
