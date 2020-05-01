using System;
namespace Template.Domain.SeedWork
{
    internal static class Require
    {
        public static void That(bool result, string message)
        {
            if (!result)
            {
                throw new DomainRuleViolationException(message);
            }
        }
    }
}
