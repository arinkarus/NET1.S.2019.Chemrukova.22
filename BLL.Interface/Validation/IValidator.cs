using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Validation
{
    public interface IValidator<in T>
    {
        bool IsValid(T item);
    }
}
