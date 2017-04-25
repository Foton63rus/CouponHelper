namespace FotonSoft.Interfaces
{
    interface IMailGenerator
    {
        /// <summary>
        ///метод, который позволяет генерировать почтовые ящики
        /// </summary>
        void generateMail();
        /// <summary>
        ///метод позволяющий сохранить сгенерированную пару login / pass
        /// </summary>
        void saveLoginPassPair();
    }
}
