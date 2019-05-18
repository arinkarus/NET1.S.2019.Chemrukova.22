using BLL.Interface.Service;
using DependencyResolver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        { 
            IKernel kernel = new StandardKernel(new ConfigModule());
            IDocumentService documentService = kernel.Get<IDocumentService>();
            documentService.Process();
        }
    }
}
