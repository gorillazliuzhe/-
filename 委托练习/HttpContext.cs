using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 委托练习
{
    public class DefaultHttpContext : HttpContext
    {
        public DefaultHttpContext()
        {
            Request = new DefaultHttpRequest();
        }
        public override HttpRequest Request
        {
            get;
            //get 这么写就不用构造函数的方式了
            //{
            //    return new DefaultHttpRequest();
            //}
        }
    }
    public class DefaultHttpRequest : HttpRequest
    {
        public override string uRL
        {
            get
            {
                return "liuzheurl";
            }
        }
    }
    public abstract class HttpContext
    {
        public abstract HttpRequest Request { get; }
    }
    public abstract class HttpRequest
    {
        public abstract string uRL { get; }
    }



    public class DefaultHttpContext1 : HttpContext
    {
        public DefaultHttpContext1()
        {
            Request = new DefaultHttpRequest1(this);
        }
        public override HttpRequest Request
        {
            get;
            //get 这么写就不用构造函数的方式了
            //{
            //    return new DefaultHttpRequest();
            //}
        }
    }
    public class DefaultHttpRequest1 : HttpRequest
    {
        public DefaultHttpRequest1(DefaultHttpContext1 context)
        {
            uRL = "liuzhe";
        }
        public override string uRL
        {
            get;
        }
    }
}
