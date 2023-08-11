using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Service
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public PagedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            // Làm tròn tổng số trang bằng cách sử dụng hàm Ceiling
            // Do hàm này là kiểu double nên chúng ta phải parse vên int
            // Hàm này nhận tham số kiểu double nên chúng ta phải ép kiểu 1 trong 2 thằng sang double
            // để tránh việc số nguyên (int) chia số nguyên (int)
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);

            // Về cơ bản PagedList kế thưa từ List nên nó sẽ như list.
            // Chúng ta cần phải dùng hàm AddRange để nó thêm (hoặc có thể hiểu là return) danh sách các item mới đc thêm vào danh sách
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
