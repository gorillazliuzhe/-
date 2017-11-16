using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托练习
{
    public interface IHttplication<TContext>
    {

    }
    class HttpApplication:IHttplication<TContext>
    {
       
    }

    public class TContext
    {
    }
}
