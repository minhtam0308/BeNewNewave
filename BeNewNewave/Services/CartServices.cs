using AutoMapper;
using BeNewNewave.Data;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Services;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Backend.Sevices
{
    public class CartServices : BaseService<Cart>, ICartServices
    {
        private IBookRepository _bookRepository;
        private ICartRepository _cartRepository;
        private ICartBookRepository _cartBookRepository;
        private readonly ResponseDto _response = new ResponseDto();
        private readonly IMapper _mapper;
        public CartServices(ICartRepository cartRepo,IBookRepository bookRepo, ICartBookRepository cartBookRepo , IMapper mapper) : base(cartRepo)
        {
            _mapper = mapper;
            _cartRepository = cartRepo;
            _bookRepository = bookRepo;
            _cartBookRepository = cartBookRepo;
        }

        public ResponseDto PostAddCart(AddBookToCartRequest request, Guid idUser)
        {

            if (!Guid.TryParse(request.IdBook, out Guid idBook))
                return _response.GenerateStrategyResponseDto("userError");

            var bookInfor = _bookRepository.GetById(idBook);
            if (bookInfor == null || bookInfor.AvailableCopies < request.Quantity)
                return _response.GenerateStrategyResponseDto("userError");

            var checkCart = _cartRepository.GetByIdUser(idUser);
            if (checkCart == null)
            {
                //cart is not exist
                var newCart = new Cart() { IdUser = idUser };
                _cartRepository.Insert(newCart, idUser.ToString());
                    
                var newCartBook = new CartBook() { IdCart = newCart.Id, IdBook = idBook, Quantity = request.Quantity };
                _cartBookRepository.Insert(newCartBook, idUser.ToString());
                _cartRepository.SaveChanges();
            }
            else
            {
                var cartBookExist = _cartBookRepository.GetByIdBookAndIdCart(checkCart.Id, idBook);
                if (cartBookExist == null)
                {
                    var newCartBook = new CartBook() { IdCart = checkCart.Id, IdBook = idBook, Quantity = request.Quantity };
                    _cartBookRepository.Insert(newCartBook, idUser.ToString());

                }
                else
                {
                    cartBookExist.Quantity = cartBookExist.Quantity + request.Quantity;
                }
                _cartBookRepository.SaveChanges();
            }

            return _response.GenerateStrategyResponseDto("success");
        }
        public ResponseDto GetAllCart(Guid idUser)
        {
 
            var cart = _cartRepository.GetCartDetail(idUser);
            if (cart == null)
            {
                cart = new Cart() { IdUser = idUser };
                _cartRepository.Insert(cart, idUser.ToString());
                _cartRepository.SaveChanges();
            }

            AllCartResponse result = new AllCartResponse()
            {
                IdCart = cart.Id,
                ListBook = new List<BookResponse>()
            };

            if(cart.CartBooks != null && cart.CartBooks.Count != 0)
            {
                foreach(var cartBook in cart.CartBooks)
                {
                    var booktemp = _bookRepository.GetById(cartBook.IdBook);
                    if (booktemp != null) {
                        BookResponse resultBook = _mapper.Map<BookResponse>(booktemp);
                        result.ListBook.Add(resultBook);
                    }
                }
            }
            _response.SetResponseDtoStrategy(new Success("get success", result));
            return _response.GetResponseDto();

        }
    }
}
