namespace MachineParameters.Services.DB
{
    public interface IDbInitialize
    {
        bool CheckIfExists();
        bool Initialize();
    }
}