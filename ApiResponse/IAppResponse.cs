using System.Collections.Generic;

namespace WebAppMovie.ApiResponse
{
    public interface IAppResponse
    {
        public void AddValidationError(ApiValidationError apiValidationError);

        public bool IsValid();

        public List<ApiValidationError> ValidationErrors();

        public T Write<T>(T res);
       
    }
}

