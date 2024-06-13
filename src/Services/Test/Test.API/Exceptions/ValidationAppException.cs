
namespace Test.API.Exceptions
{
    public class ValidationAppException:Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationAppException(IReadOnlyDictionary<string, string[]> errors)
            : base("one or more errors occured")
        {
            Errors = errors;
        }
           
   
    }
}
