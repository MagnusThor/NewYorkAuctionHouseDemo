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
using NewYork.Core.Model;
using NewYork.Core.Interfaces.Service;
using NewYork.Core.Interfaces.Data;

namespace NewYork.Service
{ 
	//NOTE:
	//If you need to implement your own logic/code do it in a partial class,
	//modifications in this file may be overwritten.
    public partial class AuctionService : BaseService<Auction>, IAuctionService
    {
		protected new IAuctionRepository Repository;				
		
		public AuctionService(IUnitOfWork unitOfWork, IAuctionRepository auctionRepository):base(unitOfWork)
		{
		    base.Repository = Repository = auctionRepository;
		}		
		//Implement custom code in a partial class
	}
}