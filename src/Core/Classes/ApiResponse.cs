using System.Collections.Generic;

namespace MC.eSIS.Core.Classes
{
    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }

        public static ApiResponse<T> Success(T result)
        {
            return new ApiResponse<T>()
            {
                Result = result,
                IsSuccess = true
            };
        }

        public new static ApiResponse<T> Error(List<PropertyError> errors)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public List<PropertyError> Errors { get; set; }

        public static ApiResponse Success()
        {
            return new ApiResponse()
            {
                IsSuccess = true
            };
        }

        public static ApiResponse Error(List<PropertyError> errors)
        {
            return new ApiResponse()
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }
}