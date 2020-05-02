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

        public static void ThatNotNullOrEmpty(string value, string message)
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new DomainRuleViolationException(message);
            }
        }
    }
}
