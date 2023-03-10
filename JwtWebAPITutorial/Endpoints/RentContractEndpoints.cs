namespace JwtWebAPITutorial.Endpoints;

public class RentContractEndpoints
{
    //public const string Area = "";
    //public const string Base = Area + "/contractgroup";
    //public const string UpdateRentContract = Base + "/update-rent-contract/{groupId}";
    //public const string DeleteRentContract = Base + "/delete-rent-contract/{id}";
    //public const string ExportRentContract = Base + "/export-rent-contract/{groupId}";

    public const string Area = "";
    public const string Base = Area + "/rentcontract";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByContractGroupId = Base + "/get-by-contractGroupId/{contractGroupId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
    public const string UpdateContractStatus = Base + "/update-contract-status/{id}";
}
