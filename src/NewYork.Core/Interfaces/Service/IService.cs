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
//IMPORTANT - Modifications to this file may be overwritten:
//If you need to implement your own logic/code do it in a partial class/interface.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using NewYork.Core.Interfaces.Validation;
	using NewYork.Core.Interfaces.Paging;
	
namespace NewYork.Core.Interfaces.Service
{
    /// <summary>
    /// Generic base interface for all services.
    /// Purpose:
    /// - Implement BusinessLogic in services
    /// - Hide IRepository from being exposed further out than service layer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ServiceContract]
    public interface IService<T>
    {
        [OperationContract]
        IQueryable<T> GetAll();
		
		[OperationContract]
		IQueryable<T> GetAllReadOnly();

        [OperationContract]
        T GetById(int id);

        [OperationContract]
        IValidationContainer<T> SaveOrUpdate(T entity);

        [OperationContract]
        void Delete(T entity);

        [OperationContract]
        IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100);
		
		[OperationContract]
        IPage<T> Page(int page = 1, int pageSize = 10);
    }
}