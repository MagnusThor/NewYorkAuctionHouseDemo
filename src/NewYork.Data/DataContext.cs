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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using NewYork.Core.Model;
	using NewYork.Core.Interfaces.Data;
	
namespace NewYork.Data
{
    public class DataContext : DbContext, IDataContext
    {
        //If you do not use the scaffolders, register your domainmodel entities here.
        //public DbSet<T> EntityNames { get; set; }        

        public DataContext()
            : this(true)
        {
        }

        public DataContext(bool proxyCreation = true)            
        {
            this.Configuration.ProxyCreationEnabled = proxyCreation;
			
            //[DropAndReCreate if in debug and model is changed. ONLY FOR DEVELOPMENT!!!]			
            if (System.Diagnostics.Debugger.IsAttached)
                Database.SetInitializer(new DataSeeder());
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //Implement custom setup here... ColumnNames, Validation, Relations etc.
        //    base.OnModelCreating(modelBuilder);
        //}

        public ObjectContext ObjectContext()
        {
            return ((IObjectContextAdapter)this).ObjectContext;
        }

        public virtual IDbSet<T> DbSet<T>() where T : PersistentEntity
        {
            return Set<T>();
        }

        public new DbEntityEntry Entry<T>(T entity) where T : PersistentEntity
        {
            return base.Entry(entity);
        }

        public DbSet<Auction> Auctions { get; set; }

        public DbSet<Bids> Bids { get; set; }
    }
    
    public class DataSeeder : DropCreateDatabaseIfModelChanges<DataContext>
    {
        //protected override void Seed(DataContext context)
        //{            
			//Insert data to your context here to add data when the database is created!
        //}       
    }
}