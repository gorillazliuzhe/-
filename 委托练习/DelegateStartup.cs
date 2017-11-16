using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托练习
{
    public class DelegateStartup
    {
        // Func<string, string> f = HelloEnglish;//实现委托 
        private Action<Middleware> _configure;

        public DelegateStartup(Action<Middleware> configure)
        {
            _configure = configure;
        }
        //调用构造函数注册的方法 a(9) 相当于调用Middle 方法
        public void Configure(Middleware app)
        {
            //相当于调用Middle 方法  
            //string s= f("Srping ji")//调用委托,实际是调用委托的实现方法
            _configure(app);
        }
    }
}
