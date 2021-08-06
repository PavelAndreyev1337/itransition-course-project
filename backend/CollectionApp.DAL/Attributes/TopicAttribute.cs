using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CollectionApp.DAL.Attributes
{
    class TopicAttribute : ValidationAttribute
    {
        private string[] _topics;

        public TopicAttribute(string[] topics)
        {
            _topics = topics;
        }

        public override bool IsValid(object value)
        {
            return value != null && _topics.Contains(value.ToString());
        }
    }
}
