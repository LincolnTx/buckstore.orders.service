namespace buckstore.orders.service.api.v1.ResponseDtos
{
    public class BaseResponseDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }

        public BaseResponseDto(bool success, T data)
        {
            Success = success;
            Data = data;
        }
    }
}