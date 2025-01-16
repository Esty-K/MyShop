using AutoMapper;
using DTO;
using Entity;

namespace MyShop
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<PostUserDTO, User>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<PostOrderDTO,Order>();
            CreateMap<OrderItemDTO,OrderItem>().ReverseMap();

            
        }
    }
}
