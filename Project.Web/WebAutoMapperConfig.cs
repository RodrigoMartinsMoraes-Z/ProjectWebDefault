using AutoMapper;

using Project.Domain.People;
using Project.Domain.Products;
using Project.Domain.Shopping;
using Project.Domain.Users;
using Project.Web.Models.Images;
using Project.Web.Models.People;
using Project.Web.Models.Product;
using Project.Web.Models.Shopping;
using Project.Web.Models.Users;

using System;

namespace Project.Web
{
    public class WebAutoMapperConfig : Profile
    {
        public WebAutoMapperConfig()
        {
            User();
            Person();
            Image();
            Product();
        }

        private void Product()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();

            CreateMap<ShoppingList, ShoppingListModel>();
            CreateMap<ShoppingListModel, ShoppingList>();

            CreateMap<Item, ItemModel>();
            CreateMap<ItemModel, Item>();

            CreateMap<CartProduct, Product>();

        }

        private void Image()
        {
            CreateMap<Image, ImageModel>();
            CreateMap<ImageModel, Image>();
        }

        private void Person()
        {
            CreateMap<Person, PersonModel>();
            CreateMap<PersonModel, Person>();
        }

        private void User()
        {
            CreateMap<User, UserModel>()
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserModel, User>();
        }
    }
}
