namespace Alkaline64.Injectable.Testables.Accessibility
{
    [Injectable<IPublicInterface>(Lifetime.Transient)]
    internal class InternalImplementation : IPublicInterface
    {
    }
}
