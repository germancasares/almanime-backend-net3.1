namespace Domain.VMs.Derived
{
    public class ModelWithMetaVM<T>
    {
        public PaginationMetaVM Meta { get; set; }
        public T Models { get; set; }
    }
}
