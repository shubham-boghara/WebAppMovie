using System.Collections.Generic;

namespace WebAppMovie.ApiResponse
{
    public class AppResponse : IAppResponse
    {
        List<ApiValidationError> validationErrors;
        public AppResponse() {

            this.validationErrors = new List<ApiValidationError>();
        }
        public void AddValidationError(ApiValidationError apiValidationError)
        {
            validationErrors.Add(apiValidationError);
        }

        public bool IsValid()
        {
            return validationErrors.Count == 0;
        }

        public List<ApiValidationError> ValidationErrors()
        {
            return this.validationErrors;
        }

        public T Write<T>(T res)
        {
            return res;
        }

       
    }
}
