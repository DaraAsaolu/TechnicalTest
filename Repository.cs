using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interview
{
    public class Repository<T> : IRepository<T> where T : IStoreable
    {
        private List<T> allItems;
        public Repository()
        {
            allItems = new List<T>();

        }
        public IEnumerable<T> All()
        {
            return allItems;
        }
        public void Delete(IComparable id)
        {
            CheckIfIdIsNotNull(id);

            allItems.RemoveAll(obj => obj.Id.Equals(id));

        }
        public void Save(T item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            CheckIfIdIsNotNull(item.Id);


            if (allItems.FirstOrDefault(obj => obj.Id.Equals(item.Id)) != null) 
            {

                Delete(item.Id);

            }
            
            allItems.Add(item);
        }
        public T FindById(IComparable id)
        {
            CheckIfIdIsNotNull(id);

            return allItems.Find(obj => obj.Id.Equals(id));
            
        }
        private void CheckIfIdIsNotNull(IComparable id)
        {
            if (id == null)
            {
                throw new ArgumentException("Id must not be null");
            }
        }

    }
}
