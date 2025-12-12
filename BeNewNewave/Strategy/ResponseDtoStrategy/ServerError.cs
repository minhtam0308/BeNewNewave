using BeNewNewave.DTOs;
using BeNewNewave.Interface.Strategy;

namespace BeNewNewave.Strategy.ResponseDtoStrategy
{
    public class ServerError : IResponseDtoStrategy
    {
        private string _messageServerError = "Server error contact to admin";
        public ServerError() { }
        public ServerError(string messageServerError) 
        {
            _messageServerError = messageServerError;
        }
        public ResponseDto GetResponse()
        {
            return new ResponseDto()
            {
                errorCode = 1,
                errorMessage = _messageServerError,
            };
        }
    }
}
