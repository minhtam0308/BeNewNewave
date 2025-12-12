using BeNewNewave.Interface.Strategy;
using BeNewNewave.Strategy.ResponseDtoStrategy;

namespace BeNewNewave.DTOs
{
    public class ResponseDto
    {
        private IResponseDtoStrategy _responseDto = new ServerError();
        public int errorCode { get; set; } = 0;
        public string errorMessage { get; set; } = "success";
        public object data { get; set; } = new();



        public IResponseDtoStrategy GetResponseDtoStrategy()
        {
            return _responseDto; 
        }

        public void SetResponseDtoStrategy(IResponseDtoStrategy value)
        {
            _responseDto = value;
        }

        public ResponseDto GetResponseDto() 
        {
            return _responseDto.GetResponse();
        }

    }
}
