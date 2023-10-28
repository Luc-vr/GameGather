namespace Infrastructure.Seed
{
    public interface ISeedData
    {
        void EnsurePopulated(bool dropExisting = false);
    }
}
