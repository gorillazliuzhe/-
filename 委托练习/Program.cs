using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using 委托练习;
using static 委托练习.GetName;


namespace 委托练习
{
    //定义委托，它定义了可以代表的方法的类型
    public delegate void GreetingDelegate(string name);
    public delegate string GetNameDelegate(string name);

    class Persion
    {
        public string name { get; set; }
        public int Age { get; set; }

        public string wan()
        {
            return "wan";
        }
    }
    class Program
    {

        private static readonly IList<Func<GetNameDelegate, GetNameDelegate>> _components = new List<Func<GetNameDelegate, GetNameDelegate>>();
        #region 实现委托的方法
        /// <summary>
        /// 实现委托
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        public static int Temp(List<bool> bin)
        {
            int i = 0;
            foreach (var item in bin)
            {
                if (item)
                {
                    i++;
                }
            }
            return i;
        }
        #endregion
        public static string returnname(string name)
        {
            return name + "哈哈";
        }
        public static string returnnameEnglish(string name)
        {
            return name + "HAHA";
        }
        //Func<RequsetDelegate, RequsetDelegate>
        public static RequsetDelegate _nexte1;//这个实际上是在build的时候给赋值了  这个下一个委托就是T2

        public static RequsetDelegate _nexte2;//如果是最后一个就是
                                              /*RequsetDelegate app = context =>
                                                                                        {
                                                                                            context = "404";
                                                                                            return Task.FromResult(0);
                                                                                        };*/


        /*public static string HelloEnglish(string strEnglish)  
                        {  
                             return "Hello." + strEnglish;  
                        }  
                        Func<string, string> f = HelloEnglish;//实现委托 
                        string s= f("Srping ji")//调用委托,实际是调用委托的实现方法
                        Console.WriteLine(s); */
        //理解管道的过程:mi1相当于:HelloEnglish,
        //Func<RequsetDelegate, RequsetDelegate> context1 = mi1;相当于Func<string, string> f = HelloEnglish
        //RequsetDelegate app=component(app)在(Middleware类里面Build 里面的循环)相当于string s=f("Srping ji")
        //t1相当于:HelloEnglish(因为又是一个委托fun<string,task>)
        //RequsetDelegate app = midd.Build();midd.Build()返回的是t1; 相当于Func<string, string> f = HelloEnglish
        //app(name)相当于:f("Srping ji")
        public static RequsetDelegate mi1(RequsetDelegate next)
        {
            //next是T2
            _nexte1 = next;//这个地方就是 build循环的时候给赋值了
            return t1;
        }
        //mi1的具体实现
        public static Task t1(string name)
        {
            //第一个中间件的对应的想处理的请求的方法
            Console.WriteLine(name + "1");
            //调用第二个中间件处理方法继续处理,这样就变成了处理链
            return _nexte1(name);//t2是_nexte1委托的实现
        }
        public static RequsetDelegate mi2(RequsetDelegate next)
        {
            //这个是最后的给的默认的
            _nexte2 = next;//这个地方就是 build循环的时候给赋值了
            return t2;
        }
        public static Task t2(string name)
        {
            //第一个中间件的对应的想处理的请求的方法
            Console.WriteLine(name + "2");
            //调用第二个中间件处理方法继续处理,这样就变成了处理链
            return _nexte2(name);
        }

        public static void action(int i)
        {
            Console.WriteLine(i);
        }
        public static void Middle(Middleware mi)
        {
            Func<RequsetDelegate, RequsetDelegate> context1 = mi1;
            Func<RequsetDelegate, RequsetDelegate> context2 = mi2;
            mi.Use(context1);
            mi.Use(context2);
        }
        static void Main(string[] args)
        {
            
            Action<int> a = action;
            a(9);

            #region 监听测试
            //string[] pric = new string[] { "http://localhost:3721/" };
            //SimpleListenerExample(pric);
            #endregion

            #region 抽象类测试
            //HttpContext hc = new DefaultHttpContext();
            //HttpRequest hr = new DefaultHttpRequest();
            //string urk = hr.uRL;
            //string url= hc.Request.uRL;

            #endregion

            #region 特性测试
            //声明一个接口实例，但不是对接口进行实例化
            //IFeatureCollection _feature = new FeatureCollection();
            //_feature.Set(new Persion());
            //var d = _feature.Get<Persion>();
            //string s = d.wan();
            #endregion

            #region 管道测试 和 下面的Startup测试 是结合的
            //Middleware midd = new Middleware();
            #region test
            //同步
            //Func<RequsetDelegate, RequsetDelegate> context3 = next =>
            //{
            //    return  context =>
            //    {
            //        //app("刘哲");调用委托 执行绑定在委托上的方法
            //         Task.Run(() => Console.WriteLine(context + "1"));
            //         return next(context);
            //    };
            //};
            //异步
            //Func<RequsetDelegate, RequsetDelegate> context4 = next =>
            //{
            //    return async context =>
            //    {
            //        //app("刘哲");调用委托 执行绑定在委托上的方法
            //        await Task.Run(() => Console.WriteLine(context + "1"));
            //        await next(context);
            //    };
            //};
            //Func<RequsetDelegate, RequsetDelegate> context4 = next =>
            //{
            //    return async context =>
            //    {
            //        await Task.Run(() => Console.WriteLine(context + "2"));
            //        //await next(context);
            //    };
            //};
            //Func<RequsetDelegate, RequsetDelegate> context1 = mi1;
            //Func<RequsetDelegate, RequsetDelegate> context2 = mi2;
            //midd.Use(context1);
            //midd.Use(context2);
            //midd.Use(next =>//next也就是T2
            //{
            //    return async context =>
            //    {
            //        await Task.Run(() => Console.WriteLine(context + "3"));
            //        await next(context);//实现T2
            //    };
            //});
            //midd.Use(next =>//next是上一个委托,这里是最后一个是默认的空处理
            //{
            //    return async context =>
            //    {
            //        await Task.Run(() => Console.WriteLine(context + "4"));
            //        await next(context);
            //    };
            //});
            //RequsetDelegate app = midd.Build();//返回的是T1
            //app("刘哲");//调用t1 启动管道
            #endregion

            #endregion

            #region Startup测试
            Middleware middware = new Middleware();
            //相当于app项目中IWebHostBuilder Configure中的new DelegateStartup(configure)          
            DelegateStartup ds = new DelegateStartup(Middle);
            ds.Configure(middware);//也就是调用注册中间件(Middle 方法里面的use)方法use 相当于a(9);  
            RequsetDelegate app = middware.Build();//返回的是T1 相当于Func<string, string> f = HelloEnglish
            app("刘哲");//调用t1 启动管道 启动中间件方法
            #endregion

            #region 调用委托
            //var list = new MyList();
            //list._List.Add(true);
            //list._List.Add(false);
            //list._List.Add(true);
            //int count = list._List.Count;
            ////实现委托
            //list.FuncAllLen += Temp;//实现将方法赋值给委托属性
            //Console.WriteLine(list.AllLen().ToString());//调用委托
            #endregion


            Console.ReadKey();

        }
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            // Note: The GetContext method blocks while waiting for a request. 
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            listener.Stop();
        }

        public static GetNameDelegate middle(GetNameDelegate next)
        {
            GetNameDelegate thismid = returnnameEnglish;
            return thismid;
        }
        public static GetNameDelegate middle1(GetNameDelegate next)
        {
            GetNameDelegate thismid = returnname;
            return thismid;
        }
        public static int TsetMothod(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 99;
            }
            return 0;

        }
        public static int TsetMothod1(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 98;
            }
            return 1;

        }
    }
    //委托返回void
    public class GreetingManager
    {
        //在GreetingManager类的内部声明delegate1变量
        public GreetingDelegate delegate1;

        public void GreetPeople(string name)
        {
            delegate1?.Invoke(name);      //通过委托调用方法
        }
    }
    //如果委托有返回值,那么会被最后一个替代
    public class GetName
    {
        //private string _name;
        //public GetName(string name)
        //{
        //    _name = name;
        //}

        public GetNameDelegate getnamedelegate { get; set; }
        //public GetNameDelegate getnamedelegate;//字段也可以

        public string GetNameFunction(string name)
        {
            if (getnamedelegate != null)//如果有方法注册委托变量
            {
                return getnamedelegate(name);//通过委托调用方法
            }
            return "错误";
        }

    }
    /// <summary>
    /// 用委托,方便大家不同方法实现
    /// </summary>
    public class MyList
    {
        public List<bool> _List = new List<bool>();
        //写一个委托,谁愿意做什么操作就自己写去,哥不管了!
        public delegate int delegateAllLen(List<bool> list);
        //写一个委托,谁愿意做什么操作就自己写去,哥不管了!
        public delegateAllLen FuncAllLen { get; set; }
        public int AllLen()
        {
            if (FuncAllLen != null)
            {
                return FuncAllLen(_List);
            }
            return 0;
        }
    }

    public interface ITestin
    {
        void itest1();
    }

    public class Isx : ITestin
    {
        public void itest1()
        {
            Console.WriteLine("i1");
        }
    }

    public class Isx1 : ITestin
    {
        public void itest1()
        {
            Console.WriteLine("i2");
        }
    }
}
