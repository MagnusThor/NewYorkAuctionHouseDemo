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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NewYork.Core.Interfaces.Validation;
	using NewYork.Core.Model;
	
namespace NewYork.Core.Common.Validation
{
    public static class ValidationEngine
    {
        /// <summary>
        /// Will validate an entity that implements IValidatableObject and DataAnnotations
        /// </summary>
        /// <typeparam name="T">The type that inherits the abstract basetype PersistentEntity</typeparam>
        /// <param name="entity">The Entity to validate</param>
        /// <returns></returns>
        public static IValidationContainer<T> GetValidationContainer<T>(this T entity) where T : PersistentEntity
        {
            var brokenrules = new Dictionary<string, IList<string>>();

            //IValidatableObject
            var customErrors = entity.Validate(new ValidationContext(entity, null, null));
            if(customErrors != null)
            foreach (var customError in customErrors){
            	if(customError == null) continue;
                foreach (var memberName in customError.MemberNames)
                {
                    if (!brokenrules.ContainsKey(memberName))
                        brokenrules.Add(memberName, new List<string>());
                    brokenrules[memberName].Add(customError.ErrorMessage);
                }
            }
            
            //DataAnnotations
            foreach (var pi in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                foreach (var attribute in (ValidationAttribute[])pi.GetCustomAttributes(typeof(ValidationAttribute), false))
                {
                    if (attribute.IsValid(pi.GetValue(entity, null))) continue;
                    if (!brokenrules.ContainsKey(pi.Name))
                        brokenrules.Add(pi.Name, new List<string>());
                    brokenrules[pi.Name].Add(attribute.FormatErrorMessage(pi.Name));
                }

            return new ValidationContainer<T>(brokenrules,entity);
        }
    }
}
