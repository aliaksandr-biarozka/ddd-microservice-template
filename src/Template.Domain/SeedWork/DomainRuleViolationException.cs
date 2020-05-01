using System;
namespace Template.Domain.SeedWork
{
    public class DomainRuleViolationException : ApplicationException
    {
        public DomainRuleViolationException(string message) : base(message) { }

        public DomainRuleViolationException(string message, Exception ex) : base(message, ex) { }
    }
}
