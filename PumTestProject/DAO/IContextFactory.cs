namespace PumTestProject.DAO
{
    public interface IContextFactory
    {
        PumContext CreateContext();
    }
}