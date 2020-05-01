//using System.Threading.Tasks;
//using Microsoft.Azure.WebJobs.Host.Bindings;

//namespace Jobs.Security
//{
//    /// <summary>
//    /// Provides a new binding instance for the function host.
//    /// </summary>
//    public class AccessTokenBindingProvider : IBindingProvider
//    {
//        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
//        {
//            IBinding binding = new AccessTokenBinding();
//            return Task.FromResult(binding);
//        }
//    }
//}