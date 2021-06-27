using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.V1.Events
{
    public class PublishModel<TData>
    {
        public string Method => "publish";

        public PublishPropsModel<TData> Params { get; set; }
    }

    public class PublishPropsModel<T>
    {
        public string Channel { get; set; }

        public T Data { get; set; }
    }
}
