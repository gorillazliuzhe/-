using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace 委托练习
{
    public delegate Task RequsetDelegate(string context);
    public class Middleware
    {
        IList<Func<RequsetDelegate, RequsetDelegate>> _components = new List<Func<RequsetDelegate, RequsetDelegate>>();

        public Middleware Use(Func<RequsetDelegate, RequsetDelegate> middle)
        {
            _components.Add(middle);
            return this;
        }
        public RequsetDelegate Build()
        {
            RequsetDelegate app = context =>
            {
                context = "404";
                return Task.FromResult(0);
            };
            foreach (var component in _components.Reverse())
            {
                /*public static string HelloEnglish(string strEnglish)  
                       {  
                            return "Hello." + strEnglish;  
                       }  
                       Func<string, string> f = HelloEnglish;//实现委托 
                       string s= f("Srping ji")//调用委托,实际是调用委托的实现方法*/
                //相当于 string s= f("Srping ji") 相当于调用mi系列方法 返回t1.返回实现mi系列方法的实现,比如t1,t2
                app = component(app);
            }
            return app;
        }
    }
}
