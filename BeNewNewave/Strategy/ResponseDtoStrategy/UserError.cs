using BeNewNewave.DTOs;
using BeNewNewave.Interface.Strategy;

namespace BeNewNewave.Strategy.ResponseDtoStrategy
{
    public class UserError : IResponseDtoStrategy
    {
        private string _messageUserError = "Your data does not meet the requirements";
        public UserError() { }
        public UserError(string messageUserError) 
        {
            _messageUserError = messageUserError;
        }
        public ResponseDto GetResponse()
        {
            return new ResponseDto()
            {
                errorCode = 2,
                errorMessage = _messageUserError,
            };
        }
    }
}
