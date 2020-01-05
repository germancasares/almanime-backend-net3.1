using FluentValidation;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Presentation.Validators
{
    public static class HelperValidators
    {
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {
            return ruleBuilder.Must(list => list.Count < num).WithMessage("The list contains too many items");
        }

        public static bool IsImage(this IFormFile file) => file.ContentType == "image/png" || file.ContentType == "image/jpg" || file.ContentType == "image/jpeg";

        public static bool IsSubtitle(this IFormFile file)
        {
            try
            {
                file.GetSubtitleFormat();

                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
