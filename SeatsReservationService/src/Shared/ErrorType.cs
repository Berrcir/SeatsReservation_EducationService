namespace Shared
{
    public enum ErrorType
    {
        /// <summary>
        /// Ошибка валидации.
        /// </summary>
        Validation,

        /// <summary>
        /// Ошибка элемент не найден.
        /// </summary>
        NotFound,

        /// <summary>
        /// Ошибка сервера.
        /// </summary>
        Failure,

        /// <summary>
        /// Ошибка конфликт.
        /// </summary>
        Conflict,
    }
}