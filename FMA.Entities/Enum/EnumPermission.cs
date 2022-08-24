using System.ComponentModel.DataAnnotations;

namespace FMA.Entities.Enum;

public enum EnumPermission
{
    NotSet = 0, //error condition

    [Display(GroupName = "Account", Name = "Get List", Description = "Get list Account")]
    AccountGetList = 10,
    [Display(GroupName = "Account", Name = "Get By Id", Description = "Get Account by Id")]
    AccountGetById = 15,
    [Display(GroupName = "Account", Name = "Add Account", Description = "Add new Account")]
    AccountAdd = 20,
    [Display(GroupName = "Account", Name = "Account Update", Description = "Update Account")]
    AccountUpdate = 25,
    [Display(GroupName = "Account", Name = "Delete Account", Description = "Delete Account")]
    AccountDelete = 30,


    [Display(GroupName = "Product", Name = "Get List", Description = "Get list product")]
    ProductGetList = 50,
    [Display(GroupName = "Product", Name = "Get By Id", Description = "Get product by Id")]
    ProductGetById = 55,
    [Display(GroupName = "Product", Name = "Add Product", Description = "Add new product")]
    ProductAdd = 60,
    [Display(GroupName = "Product", Name = "Product Update", Description = "Update product")]
    ProductUpdate = 65,
    [Display(GroupName = "Product", Name = "Delete Product", Description = "Delete product")]
    ProductDelete = 70,

    [Display(GroupName = "Category", Name = "Get List", Description = "Get list Category")]
    CategoryGetList = 90,
    [Display(GroupName = "Category", Name = "Get By Id", Description = "Get Category by Id")]
    CategoryGetById = 95,
    [Display(GroupName = "Category", Name = "Add Category", Description = "Add new Category")]
    CategoryAdd = 100,
    [Display(GroupName = "Category", Name = "Category Update", Description = "Update Category")]
    CategoryUpdate = 105,
    [Display(GroupName = "Category", Name = "Delete Category", Description = "Delete Category")]
    CategoryDelete = 110,
}