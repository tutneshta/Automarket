using Automarket.Domain.Enam;

namespace Automarket.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        /// <summary>
        /// запись ошибки
        /// </summary>
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }
        /// <summary>
        /// запись результатов запросов
        /// </summary>
        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
        StatusCode StatusCode { get; }
    }
}