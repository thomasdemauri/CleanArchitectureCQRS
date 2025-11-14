namespace CleanArchitecture.Domain.Shared
{
    public abstract class AgreggateRoot<Tkey>
    {
        public Tkey Id { get; protected set; }

        protected AgreggateRoot(Tkey id)
        {
            Id = id;
        }
    }
}
