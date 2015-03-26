using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePeace.Common
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            Errors = new List<string>();
        }

        public List<string> Errors 
        { 
            get; 
            private set;
        }

        public virtual bool Success
        {
            get
            {
                return !Errors.Any();
            }
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public ServiceResponse()
            : base()
        {

        }

        public ServiceResponse(T response)
            : this()
        {
            Response = response;
        }

        public T Response 
        { 
            get; 
            set;
        }
    }
}
