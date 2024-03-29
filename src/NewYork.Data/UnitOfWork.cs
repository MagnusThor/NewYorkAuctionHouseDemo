/*
Copyright (c) 2012 
Ulf Björklund
http://average-uffe.blogspot.com/
http://twitter.com/codeplanner
http://twitter.com/ulfbjo

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NewYork.Core.Model;
	using NewYork.Core.Interfaces.Data;
	
namespace NewYork.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDataContext _datacontext;

        private readonly IDatabaseFactory _databaseFactory;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
            this.DataContext.ObjectContext().SavingChanges += (sender, e) => BeforeSave(this.GetChangedOrNewEntities());
        }

        public IDataContext DataContext
        {
            get { return this._datacontext ?? (this._datacontext = this._databaseFactory.Get()); }
        }

        /// <summary>
        /// Extracts new or changed entities and return them as PersistenEntities.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PersistentEntity> GetChangedOrNewEntities()
        {
            const EntityState newOrModified = EntityState.Added | EntityState.Modified;

            return this.DataContext.ObjectContext().ObjectStateManager.GetObjectStateEntries(newOrModified)
                .Where(x => x.Entity != null).Select(x => x.Entity as PersistentEntity);
        }

        /// <summary>
        /// Before save, we will set the updated and created time.
        /// We do this in save or update, but here we can reach all children that may have been edited/created.
        /// In saveOrUpdate it´s only the current entity getting accessed...
        /// </summary>
        /// <param name="entities"></param>
        public void BeforeSave(IEnumerable<PersistentEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Updated = DateTime.Now.ToString();
                entity.Created = !IsPersistent(entity) ? DateTime.Now.ToString() : entity.Created;
            }
        }

        public static bool IsPersistent(PersistentEntity entity)
        {
            return entity.Id != 0;
        }

        public int Commit()
        {
            return this.DataContext.ObjectContext().SaveChanges();
        }
    }
}
