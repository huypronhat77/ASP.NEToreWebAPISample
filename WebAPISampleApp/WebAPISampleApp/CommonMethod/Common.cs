using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Data;
using WebAPISampleApp.Model;

namespace WebAPISampleApp.CommonMethod
{
    public static class Common
    {
        #region Product
        public enum ProducSorting
        {
            Name_Desc,
            Name_Asc,
            Price_Asc,
            Price_Desc,

        }

        public static IQueryable<Product> SortProduct(IQueryable<Product> currentList, ProducSorting sorting)
        {
            if (Enum.TryParse(sorting.ToString(), out ProducSorting sortType))
            {
                switch (sortType)
                {
                    case ProducSorting.Name_Desc:
                        currentList = currentList.OrderByDescending(p => p.Name);
                        break;
                    case ProducSorting.Name_Asc:
                        currentList = currentList.OrderBy(p => p.Name);
                        break;
                    case ProducSorting.Price_Asc:
                        currentList = currentList.OrderBy(p => p.Price);
                        break;
                    case ProducSorting.Price_Desc:
                        currentList = currentList.OrderByDescending(p => p.Price);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                currentList = currentList.OrderBy(p => p.Name);
            }

            return currentList;
        }

        public static IQueryable<Product> FilterProduct(IQueryable<Product> currentList, ProductFilterOption option) 
        {
            if (option != null)
            {
                if (!string.IsNullOrEmpty(option.Name))
                {
                    currentList = currentList.Where(p => p.Name.Equals(option.Name)
                    || p.Name.Contains(option.Name));
                }

                if (option.MinPrice.HasValue)
                {
                    currentList = currentList.Where(p => p.Price >= option.MinPrice);
                }

                if (option.MaxPrice.HasValue)
                {
                    currentList = currentList.Where(p => p.Price <= option.MaxPrice);
                }

                if (!string.IsNullOrEmpty(option.category))
                {
                    currentList = currentList.Where(p => p.Category.Name.Equals(option.category)
                    || p.Category.Name.Contains(option.category));
                }
            }

            return currentList;
        }
        #endregion Product
        // Money type for each country
        public struct CountryMoneyFormat 
        {
            public const string VietNam = "vi-VN";      
        }

        /// <summary>
        /// Check if the update data is valid? if not valid keep the old data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originValue"></param>
        /// <param name="NewValue"></param>
        /// <returns></returns>
        public static T InitializeValidData<T>(T originValue, T NewValue) 
        {
            T result;

            if (int.TryParse(NewValue.ToString(), out int validNumber) && validNumber.Equals(0))
            {
                return originValue;
            }

            result = string.IsNullOrEmpty(NewValue.ToString()) ? originValue : NewValue;
            return result;
        }

        /// <summary>
        /// Format the money to display more friendly
        /// </summary>
        /// <param name="money"></param>
        /// <param name="moneyFormat"></param>
        /// <returns></returns>
        public static string FormatMoney(double money, string moneyFormat)
        {
            CultureInfo culture = new CultureInfo(moneyFormat);
            return (money.ToString("C", culture));
        }
    }
}
