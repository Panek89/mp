namespace Machines.DataAccess.EfCore.Services.DB
{
    public interface IDbInitialize
    {
        bool CheckIfExists();
        bool Initialize();
    }
}