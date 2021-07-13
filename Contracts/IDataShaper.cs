using System.Collections.Generic;
using System.Dynamic;

namespace Contracts
{
    public interface IDataShaper<T>
    {
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities,
            string fieldsString);

        public ExpandoObject ShapeData(T entity, string fieldsString);
    }
}
