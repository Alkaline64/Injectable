namespace Alkaline64.Injectable.Tests.Accessibility.Accessibility;

[Injectable<IPublicInterface>(Lifetime.Transient)]
internal class InternalImplementation : IPublicInterface
{
}
