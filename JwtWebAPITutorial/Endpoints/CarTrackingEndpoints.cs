namespace JwtWebAPITutorial.Endpoints;

public class CarTrackingEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/cartracking";
    public const string GetAll = Base;
    public const string GetSingle = Base + "/{id}";
    public const string GetByCarId = Base + "/get-by-carId/{carId}";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update/{id}";
    public const string Delete = Base + "/delete/{id}";
}
