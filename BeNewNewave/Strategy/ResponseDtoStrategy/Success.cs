using BeNewNewave.DTOs;
using BeNewNewave.Interface.Strategy;

namespace BeNewNewave.Strategy.ResponseDtoStrategy
{
    public class Success : IResponseDtoStrategy
    { 
        private string _messageSuccess = "Action success";
        private object _data;

        public Success() { }

        public Success(string messageSuccess, object data) {
            _messageSuccess = messageSuccess;
            _data = data;
        }
        public ResponseDto GetResponse()
        {
            return new ResponseDto()
            {
                errorCode = 0,
                errorMessage = _messageSuccess,
                data = _data
            };
        }
    }
}
