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
}