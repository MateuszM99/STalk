using Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Application.Comparers
{
    public class ConversationComparer : IEqualityComparer<Conversation>
    {
        public bool Equals([AllowNull] Conversation x, [AllowNull] Conversation y)
        {
            if (x.Id == y.Id)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Conversation obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
